using Budgets;
using System;
class Program
{
    static void Main(string[] args)
    {
        var budget = new Budget("valet");

        budget.AddTransaction(50.54);
        budget.AddTransaction(25.5);
        budget.AddTransaction(83.67);

        budget.ShowStatistics();
    }
}