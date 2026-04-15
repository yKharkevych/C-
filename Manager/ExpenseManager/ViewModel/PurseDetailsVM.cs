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
    public partial class PurseDetailsVM : ObservableObject, IQueryAttributable
    {
        private readonly IPurseService _purseService;
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        public partial PurseDetailsDTO? CurrentPurse { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<TransactionListDTO> Transactions { get; set; }

        public PurseDetailsVM(IPurseService purseService, ITransactionService transactionService)
        {
            _purseService = purseService;
            _transactionService = transactionService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var purseId = (Guid)query["PurseId"];
            CurrentPurse = _purseService.GetPurse(purseId);
            Transactions = new ObservableCollection<TransactionListDTO>(_transactionService.GetTransactionsByPurseId(purseId));
            OnPropertyChanged(nameof(Transactions));
        }

        [RelayCommand]
        private void LoadTransaction(Guid transactionId)
        {
            Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}", new Dictionary<string, object> { { "TransactionId", transactionId } });
        }

        
    }
}
