using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Pages;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Manager.ExpenseManager.ViewModel
{
    // ViewModel for the PurseDetailsPage, responsible for handling the logic and data for displaying purse details and its transactions.
    public partial class PurseDetailsVM : BaseViewModel, IQueryAttributable
    {
        private readonly IPurseService _purseService;
        private readonly ITransactionService _transactionService;

        private Guid _purseId;

        [ObservableProperty]
        public partial PurseDetailsDTO? CurrentPurse { get; set; }

        private List<TransactionListDTO> _allTransactions = new();

        [ObservableProperty]
        public partial ObservableCollection<TransactionListDTO> Transactions { get; set; } = new();

        [ObservableProperty]
        public partial string SearchText { get; set; } = string.Empty;

        public IReadOnlyList<string> SortOptions { get; } = new[]
        {
            "Date (new to old)",
            "Date (old to new)",
            "Amount (high to low)",
            "Amount (low to high)",
            "Category (A-Z)"
        };

        [ObservableProperty]
        public partial string SelectedSortOption { get; set; } = "Date (new to old)";

        partial void OnSearchTextChanged(string value) => ApplyFilterAndSort();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilterAndSort();

        public PurseDetailsVM(IPurseService purseService, ITransactionService transactionService)
        {
            _purseService = purseService;
            _transactionService = transactionService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("PurseId", out var purseIdObj) && purseIdObj is Guid purseId)
            {
                _purseId = purseId;
            }
            _ = LoadAsync();
        }

        [RelayCommand]
        private Task LoadAsync() => ExecuteBusyAsync(async () =>
        {
            if (_purseId == Guid.Empty)
                return;

            CurrentPurse = await _purseService.GetPurseAsync(_purseId);
            var transactions = await _transactionService.GetTransactionsByPurseIdAsync(_purseId);
            _allTransactions = transactions.ToList();
            ApplyFilterAndSort();
        });

        private void ApplyFilterAndSort()
        {
            IEnumerable<TransactionListDTO> result = _allTransactions;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var query = SearchText.Trim();
                result = result.Where(t =>
                    t.Category.ToString().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (t.AmountDesc != null && t.AmountDesc.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }

            result = SelectedSortOption switch
            {
                "Date (new to old)" => result.OrderByDescending(t => t.Date),
                "Date (old to new)" => result.OrderBy(t => t.Date),
                "Amount (high to low)" => result.OrderByDescending(t => ParseAmount(t.AmountDesc)),
                "Amount (low to high)" => result.OrderBy(t => ParseAmount(t.AmountDesc)),
                "Category (A-Z)" => result.OrderBy(t => t.Category.ToString()),
                _ => result
            };

            Transactions = new ObservableCollection<TransactionListDTO>(result);
        }

        private static decimal ParseAmount(string? amountDesc)
        {
            if (string.IsNullOrWhiteSpace(amountDesc))
                return 0;
            var cleaned = amountDesc.Replace("+", "").Trim();
            return decimal.TryParse(cleaned, System.Globalization.CultureInfo.InvariantCulture, out var result)
                ? result
                : 0;
        }

        [RelayCommand]
        private Task LoadTransactionAsync(Guid transactionId)
            => Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}", new Dictionary<string, object>
            {
                { "TransactionId", transactionId }
            });

        [RelayCommand]
        private Task EditPurseAsync()
        {
            if (CurrentPurse == null)
                return Task.CompletedTask;
            return Shell.Current.GoToAsync(nameof(PurseEditPage), new Dictionary<string, object>
            {
                { "PurseId", CurrentPurse.Id }
            });
        }

        [RelayCommand]
        private Task AddTransactionAsync()
        {
            if (_purseId == Guid.Empty)
                return Task.CompletedTask;
            return Shell.Current.GoToAsync(nameof(TransactionCreatePage), new Dictionary<string, object>
            {
                { "PurseId", _purseId }
            });
        }

        [RelayCommand]
        private Task DeleteTransactionAsync(Guid transactionId) => ExecuteBusyAsync(async () =>
        {
            var page = Shell.Current?.CurrentPage;
            if (page == null) return;

            bool confirmed = await page.DisplayAlert(
                "Delete transaction",
                "Are you sure you want to delete this transaction?",
                "Delete", "Cancel");

            if (!confirmed)
                return;

            await _transactionService.DeleteTransactionAsync(transactionId);
            await LoadAsync(); 
        });
    }
}
