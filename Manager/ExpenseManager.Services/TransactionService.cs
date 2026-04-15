using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    // Service implementation for managing transactions, responsible for retrieving transaction information and details.
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<TransactionListDTO> GetTransactionsByPurseId(Guid purseId)
        {
            foreach(var transaction in _transactionRepository.GetTransactionsByPurseId(purseId)){
                yield return new TransactionListDTO(transaction.Id, transaction.Category, transaction.Amount);
            }
        }

        public TransactionDetailsDTO GetTransactionDetails(Guid transactionId)
        {
            var transaction = _transactionRepository.GetTransactionById(transactionId)
                ?? throw new KeyNotFoundException($"Transaction with id {transactionId} not found.");

            return new TransactionDetailsDTO(
                transaction.Id,
                transaction.Category,
                transaction.Amount,
                transaction.Date,
                transaction.Description
            );
        }
    }
}
