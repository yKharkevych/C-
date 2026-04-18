using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    public partial class TransactionCreateVM : BaseViewModel, IQueryAttributable
    {
        private readonly ITransactionService _transactionService;

        private Guid _purseId;

        [ObservableProperty]
        public partial decimal Amount { get; set; }

        public EnumWithName<Category>[] Categories { get; } = EnumExtensions.GetValuesWithNames<Category>();

        [ObservableProperty]
        public partial EnumWithName<Category>? SelectedCategory { get; set; }

        [ObservableProperty]
        public partial DateTime Date { get; set; } = DateTime.Now;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        public TransactionCreateVM(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            SelectedCategory = Categories.Length > 0 ? Categories[0] : null;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("PurseId", out var purseIdObj) && purseIdObj is Guid purseId)
            {
                _purseId = purseId;
            }
        }

        [RelayCommand]
        private Task CreateAsync() => ExecuteBusyAsync(async () =>
        {
            if (SelectedCategory == null)
                throw new System.ComponentModel.DataAnnotations.ValidationException("Please select a category.");

            var dto = new TransactionCreateDTO(
                _purseId,
                Amount,
                SelectedCategory.Value,
                Date,
                Description ?? string.Empty);

            await _transactionService.CreateTransactionAsync(dto);
            await Shell.Current.GoToAsync("..");
        });

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}
