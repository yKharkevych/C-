using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.Pages;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    // ViewModel for the TransactionDetailsPage, responsible for handling the logic and data for displaying transaction details.
    public partial class TransactionDetailsVM : BaseViewModel, IQueryAttributable
    {
        private readonly ITransactionService _transactionService;

        private Guid _transactionId;

        [ObservableProperty]
        public partial TransactionDetailsDTO? CurrentTransaction { get; set; }

        public TransactionDetailsVM(ITransactionService transactionService)
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
            CurrentTransaction = await _transactionService.GetTransactionDetailsAsync(_transactionId);
        });

        [RelayCommand]
        private Task EditAsync()
        {
            if (_transactionId == Guid.Empty)
                return Task.CompletedTask;
            return Shell.Current.GoToAsync(nameof(TransactionEditPage), new Dictionary<string, object>
            {
                { "TransactionId", _transactionId }
            });
        }
    }
}
