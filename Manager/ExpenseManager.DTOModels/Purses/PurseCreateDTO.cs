using Manager.ExpenseManager.Common;
using System;

namespace Manager.ExpenseManager.DTOModels.Purses
{
    // DTO for creating a new purse. This class is used to transfer data from the client to the server when a new purse is being created.
    public class PurseCreateDTO
    {
        public string Name { get; }
        public Currency Currency { get; }
        public decimal StartBalance { get; }

        public PurseCreateDTO(string name, Currency currency, decimal startBalance)
        {
            Name = name;
            Currency = currency;
            StartBalance = startBalance;
        }
    }
}
