using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<IEnumerable<TransactionListDTO>> GetTransactionsByPurseIdAsync(Guid purseId)
        {
            var transactions = await _transactionRepository.GetTransactionsByPurseIdAsync(purseId);
            return transactions.Select(t => new TransactionListDTO(t.Id, t.Category, t.Amount, t.Date));
        }

        public async Task<TransactionDetailsDTO?> GetTransactionDetailsAsync(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction is null)
                return null;

            return new TransactionDetailsDTO(
                transaction.Id,
                transaction.Category,
                transaction.Amount,
                transaction.Date,
                transaction.Description
            );
        }

        public async Task<TransactionEditDTO?> GetTransactionForEditAsync(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction is null)
                return null;
            return new TransactionEditDTO(
                transaction.Id,
                transaction.Amount,
                transaction.Category,
                transaction.Date,
                transaction.Description
            );
        }

        public async Task CreateTransactionAsync(TransactionCreateDTO createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException(nameof(createDto));

            var errors = createDto.Validate();
            if (errors.Count > 0)
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));

            var newTransaction = new TransactionDB(
                createDto.PurseId,
                createDto.Amount,
                createDto.Category,
                createDto.Date,
                createDto.Description ?? string.Empty
            );
            await _transactionRepository.SaveTransactionAsync(newTransaction);
        }

        public async Task UpdateTransactionAsync(TransactionEditDTO editDto)
        {
            if (editDto == null)
                throw new ArgumentNullException(nameof(editDto));

            var errors = editDto.Validate();
            if (errors.Count > 0)
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));

            var existing = await _transactionRepository.GetTransactionByIdAsync(editDto.Id)
                ?? throw new KeyNotFoundException($"Transaction with id {editDto.Id} not found.");

            existing.Amount = editDto.Amount;
            existing.Category = editDto.Category;
            existing.Date = editDto.Date;
            existing.Description = editDto.Description ?? string.Empty;

            await _transactionRepository.SaveTransactionAsync(existing);
        }

        public Task DeleteTransactionAsync(Guid transactionId)
        {
            return _transactionRepository.DeleteTransactionAsync(transactionId);
        }
    }
}
