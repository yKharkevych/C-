using System;
using System.Collections.Generic;
using System.Text;

namespace Budgets
{
    class Budget
    {
        List<double> transactions;
        private string name;

        public Budget(string name)
        {
            transactions = new List<double>();
            this.name = name;
        }
        public void AddTransaction(double amount)
        {
            transactions.Add(amount);
        }

        public void ShowStatistics()
        {
            var result = 0.0;
            var lowest = 9999.99;
            var highest = 0.0;
            foreach (var amount in transactions)
            {
                result += amount;
                lowest = Math.Min(lowest, amount);
                highest = Math.Max(highest, amount);
            }
            result /= transactions.Count;
            Console.WriteLine($"average transaction is ${result:N2}");
            Console.WriteLine($"lowest transaction is ${lowest}");
            Console.WriteLine($"highest transaction is ${highest}");
        }
    }
}
