using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
 
        public async Task<IEnumerable<PurseListDTO>> GetPursesAsync()
        {
            var purses = await _purseRepository.GetPursesAsync();
            var result = new List<PurseListDTO>();
            foreach (var purse in purses)
            {
                var balance = await _transactionRepository.BalanceByPurseIdAsync(purse.Id);
                result.Add(new PurseListDTO(purse.Id, purse.Name, purse.Currency, purse.StartBalance, balance));
            }
            return result;
        }
 
        public async Task<PurseDetailsDTO?> GetPurseAsync(Guid id)
        {
            var purse = await _purseRepository.GetPurseAsync(id);
            if (purse is null)
                return null;
            var balance = await _transactionRepository.BalanceByPurseIdAsync(purse.Id);
            return new PurseDetailsDTO(purse.Id, purse.Name, purse.Currency, balance);
        }
 
        public async Task<PurseEditDTO?> GetPurseForEditAsync(Guid id)
        {
            var purse = await _purseRepository.GetPurseAsync(id);
            return purse is null ? null : new PurseEditDTO(purse.Id, purse.Name);
        }
 
        public async Task CreatePurseAsync(PurseCreateDTO createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException(nameof(createDto));
 
            var errors = createDto.Validate();
            if (errors.Count > 0)
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
 
            var newPurse = new PurseDB(createDto.Name, createDto.Currency, createDto.StartBalance);
            await _purseRepository.SavePurseAsync(newPurse);
        }
 
        public async Task UpdatePurseAsync(PurseEditDTO editDto)
        {
            if (editDto == null)
                throw new ArgumentNullException(nameof(editDto));
 
            var errors = editDto.Validate();
            if (errors.Count > 0)
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
 
            var existing = await _purseRepository.GetPurseAsync(editDto.Id)
                ?? throw new KeyNotFoundException($"Purse with id {editDto.Id} not found.");
 
            existing.Name = editDto.Name;
            await _purseRepository.SavePurseAsync(existing);
        }
 
        public Task DeletePurseAsync(Guid id)
        {
            return _purseRepository.DeletePurseAsync(id);
        }
    }
}
