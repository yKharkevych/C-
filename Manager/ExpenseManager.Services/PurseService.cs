using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    // Service implementation for managing purses, responsible for retrieving purse information and calculating balances.
    public class PurseService : IPurseService
    {
        private readonly IPurseRepository _purseRepository;
        private readonly ITransactionRepository _transactionRepository;

        public PurseService(IPurseRepository purseRepository, ITransactionRepository transactionRepository)
        {
            _purseRepository = purseRepository;
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<PurseListDTO> GetPurses()
        {
            foreach (var purse in _purseRepository.GetPurses())
            { 
                var balance = _transactionRepository.BalanceByPurseId(purse.Id);
                yield return new PurseListDTO(purse.Id, purse.Name, purse.Currency, purse.StartBalance, balance);
            }
        }

        public PurseDetailsDTO GetPurse(Guid id)
        {
            var purse = _purseRepository.GetPurse(id)
                ?? throw new KeyNotFoundException($"Purse with id {id} not found.");
            var balance = _transactionRepository.BalanceByPurseId(purse.Id);
            return new PurseDetailsDTO(purse.Id, purse.Name, purse.Currency, balance);
        }
    }
}
