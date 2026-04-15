using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Repositories;
using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.Storage;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Manager.ExpenseManager.ConsoleApp
{
    internal class Program
    {
        enum AppState
        {
            Default,
            PursesDetails,
            PurseTransactions,
            Exit
        }

        private static AppState _appState = AppState.Default;
        private static IPurseService _purseService;
        private static ITransactionService _transactionService;
        private static List<PurseListDTO> _purses;


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Expense Manager App!");
            var storageContext = new InMemoryStorageContext();
            var purseRepo = new PurseRepository(storageContext);
            var transactionRepo = new TransactionRepository(storageContext);

            _purseService = new PurseService(purseRepo, transactionRepo);
            _transactionService = new TransactionService(transactionRepo);
            int? command = 0;
            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.Default:
                        Console.WriteLine("\n Щоб подивитися інформацію про гаманці - натисніть 1.\n Щоб подивитися список транзакцій певного гаманця - натисніть 2.\n Щоб вийти із застосунку - натисніть 3. \n");
                        command = Convert.ToInt32(Console.ReadLine());
                        UpdateState(command.Value);
                        break;
                    case AppState.PursesDetails:
                        ShowPursesDetails();
                        _appState = AppState.Default;
                        break;
                    case AppState.PurseTransactions:
                        Console.WriteLine("Введіть назву гаманця, транзакції якого хочете побачити:");
                        string name = Console.ReadLine();
                        ShowPurseTransactions(name);
                        _appState = AppState.Default;
                        break;
                }
            }
        }


        private static void UpdateState(int command)
        {
            switch (command)
            {
                case 0:
                    _appState = AppState.Default;
                    break;
                case 1:
                    _appState = AppState.PursesDetails;
                    break;
                case 2:
                    _appState = AppState.PurseTransactions;
                    break;
                case 3:
                    _appState = AppState.Exit;
                    break;
                default:
                    Console.WriteLine("Невірна команда. Спробуйте ще раз.");
                    _appState = AppState.Default;
                    break;
            }
        }

        private static void LoadPurses()
        {
            if (_purses != null)
                return;
            _purses = new List<PurseListDTO>();
            foreach (var purse in _purseService.GetPurses())
            {
                _purses.Add(purse);
            }
        }

        private static void ShowPursesDetails()
        {
            LoadPurses();
            Console.WriteLine("Інформація про гаманці:");
            foreach (var purse in _purses)
            {
                Console.WriteLine(purse);
            }
        }

        private static void ShowPurseTransactions(string? name)
        { 
            bool found = false;
            foreach(var purse in _purses){
                if(purse.Name == name)
                {
                    found = true;
                    Console.WriteLine($"Транзакції для гаманця {purse.Name}:");
                    foreach(var transaction in _transactionService.GetTransactionsByPurseId(purse.Id))
                    {
                        Console.WriteLine(transaction);
                    }
                }
            }
            if (!found)
                Console.WriteLine("Такого гаманця не існує.");
        }
    }
}
