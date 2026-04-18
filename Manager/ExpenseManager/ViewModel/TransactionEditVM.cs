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
    public partial class TransactionEditVM : BaseViewModel, IQueryAttributable
    {
        private readonly ITransactionService _transactionService;

        private Guid _transactionId;

        [ObservableProperty]
        public partial decimal Amount { get; set; }

        public EnumWithName<Category>[] Categories { get; } = EnumExtensions.GetValuesWithNames<Category>();

        [ObservableProperty]
        public partial EnumWithName<Category>? SelectedCategory { get; set; }

        [ObservableProperty]
        public partial DateTime Date { get; set; } = DateTime.Now;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        public TransactionEditVM(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("TransactionId", out var transactionIdObj) && transactionIdObj is Guid transactionId)
            {
                _transactionId = transactionId;
            }
            _ = LoadAsync();
        }

        [RelayCommand]
        private Task LoadAsync() => ExecuteBusyAsync(async () =>
        {
            if (_transactionId == Guid.Empty)
                return;

            var dto = await _transactionService.GetTransactionForEditAsync(_transactionId);
            if (dto == null)
                return;

            Amount = dto.Amount;
            SelectedCategory = Categories.FirstOrDefault(c => c.Value.Equals(dto.Category));
            Date = dto.Date;
            Description = dto.Description ?? string.Empty;
        });

        [RelayCommand]
        private Task SaveAsync() => ExecuteBusyAsync(async () =>
        {
            if (SelectedCategory == null)
                throw new System.ComponentModel.DataAnnotations.ValidationException("Please select a category.");

            var dto = new TransactionEditDTO(
                _transactionId,
                Amount,
                SelectedCategory.Value,
                Date,
                Description ?? string.Empty);

            await _transactionService.UpdateTransactionAsync(dto);
            await Shell.Current.GoToAsync("..");
        });

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}
