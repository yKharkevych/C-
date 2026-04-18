using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.DTOModels.Purses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    public static class Validators
    {
        public record struct ValidationError(string ErrorMessage, string MemberName);

        public static List<ValidationError> Validate(this PurseCreateDTO dto)
        {
            var errors = new List<ValidationError>();
            errors.AddRange(ValidatePurseName(dto.Name, nameof(PurseCreateDTO.Name)));
            return errors;
        }

        public static List<ValidationError> Validate(this PurseEditDTO dto)
        {
            var errors = new List<ValidationError>();
            if (dto.Id == Guid.Empty)
                errors.Add(new ValidationError("Purse id must be set.", nameof(PurseEditDTO.Id)));
            errors.AddRange(ValidatePurseName(dto.Name, nameof(PurseEditDTO.Name)));
            return errors;
        }

        private static List<ValidationError> ValidatePurseName(string name, string propertyName)
        {
            var errors = new List<ValidationError>();
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new ValidationError("Purse name can't be empty.", propertyName));
                return errors;
            }
            if (name.Length < 2)
                errors.Add(new ValidationError("Purse name must be at least 2 characters long.", propertyName));
            if (name.Length > 50)
                errors.Add(new ValidationError("Purse name must be no longer than 50 characters.", propertyName));
            return errors;
        }

        public static List<ValidationError> Validate(this TransactionCreateDTO dto)
        {
            var errors = new List<ValidationError>();
            if (dto.PurseId == Guid.Empty)
                errors.Add(new ValidationError("Transaction must be assigned to a purse.", nameof(TransactionCreateDTO.PurseId)));
            errors.AddRange(ValidateTransactionFields(dto.Amount, dto.Date));
            return errors;
        }

        public static List<ValidationError> Validate(this TransactionEditDTO dto)
        {
            var errors = new List<ValidationError>();
            if (dto.Id == Guid.Empty)
                errors.Add(new ValidationError("Transaction id must be set.", nameof(TransactionEditDTO.Id)));
            errors.AddRange(ValidateTransactionFields(dto.Amount, dto.Date));
            return errors;
        }

        private static List<ValidationError> ValidateTransactionFields(decimal amount, DateTime date)
        {
            var errors = new List<ValidationError>();
            if (amount == 0)
                errors.Add(new ValidationError("Amount can't be zero.", "Amount"));
            if (decimal.Round(amount, 2) != amount)
                errors.Add(new ValidationError("Amount must have no more than 2 decimal places.", "Amount"));
            if (date == default)
                errors.Add(new ValidationError("Date must be selected.", "Date"));
            if (date > DateTime.Now.AddDays(1))
                errors.Add(new ValidationError("Date can't be in the future.", "Date"));
            return errors;
        }
    }
}
