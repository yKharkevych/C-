using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Manager.ExpenseManager.Storage
{
    public class FileStorageContext : IStorageContext
    {
        private readonly string _databaseRoot;
        private readonly string _pursesDir;
        private readonly string _transactionsDir;
        private readonly JsonSerializerOptions _jsonOptions;

        private readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);
        private bool _initialized;

        public FileStorageContext(string appDataDirectory)
        {
            _databaseRoot = Path.Combine(appDataDirectory, "ExpenseManagerDB");
            _pursesDir = Path.Combine(_databaseRoot, "purses");
            _transactionsDir = Path.Combine(_databaseRoot, "transactions");
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        private async Task EnsureInitializedAsync()
        {
            if (_initialized)
                return;

            await _initLock.WaitAsync();
            try
            {
                if (_initialized)
                    return;

                bool isFirstRun = !Directory.Exists(_databaseRoot);

                Directory.CreateDirectory(_pursesDir);
                Directory.CreateDirectory(_transactionsDir);

                if (isFirstRun)
                {
                    await SeedInitialDataAsync();
                }

                _initialized = true;
            }
            finally
            {
                _initLock.Release();
            }
        }

        private async Task SeedInitialDataAsync()
        {
            var cash = new PurseDB(Guid.NewGuid(), "Готівка", Currency.UAH, 1000);
            var monoUAH = new PurseDB(Guid.NewGuid(), "Монобанк", Currency.UAH, 12645);
            var monoUSD = new PurseDB(Guid.NewGuid(), "Монобанк", Currency.USD, 580);
            var cashPLN = new PurseDB(Guid.NewGuid(), "Готівка", Currency.PLN, 1730.50m);

            foreach (var purse in new[] { cash, monoUAH, monoUSD, cashPLN })
            {
                await WritePurseAsync(purse);
            }

            var transactions = new[]
            {
                new TransactionDB(Guid.NewGuid(), cash.Id,    -50,     Category.Cafe,          new DateTime(2026, 3, 7, 15, 34, 56), "Купівля кави"),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id,  25000,  Category.Salary,        new DateTime(2026, 3, 1, 14, 25, 46), "Дохід з проєкту"),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id, -1200,   Category.Clothes,       new DateTime(2026, 3, 4, 13, 36, 23), "Нові джинси"),
                new TransactionDB(Guid.NewGuid(), monoUSD.Id, -18.5m,  Category.Cafe,          new DateTime(2026, 3, 1, 15, 46, 21), "Обід"),
                new TransactionDB(Guid.NewGuid(), cashPLN.Id, -200,    Category.Entertainment, new DateTime(2026, 3, 2, 18, 45, 21), "Квитки в кіно"),
                new TransactionDB(Guid.NewGuid(), monoUSD.Id,  300,    Category.Other,         new DateTime(2026, 3, 5, 12, 54, 47), "Продаж старого телефону"),
                new TransactionDB(Guid.NewGuid(), cashPLN.Id, -368,    Category.Products,      new DateTime(2026, 3, 1, 21, 43, 21), ""),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id, -500,    Category.Transport,     new DateTime(2026, 3, 5, 8, 40, 11),  "Таксі"),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id, -1500,   Category.House,         new DateTime(2026, 3, 3, 13, 36, 23), "Комунальні послуги"),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id, -17500,  Category.House,         new DateTime(2026, 3, 4, 10, 15, 47), "Оренда"),
                new TransactionDB(Guid.NewGuid(), monoUSD.Id,  650,    Category.Salary,        new DateTime(2026, 3, 3, 15, 34, 56), "Фріланс"),
                new TransactionDB(Guid.NewGuid(), monoUSD.Id, -200,    Category.Other,         new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти"),
                new TransactionDB(Guid.NewGuid(), monoUAH.Id,  8600,   Category.Other,         new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти")
            };

            foreach (var transaction in transactions)
            {
                await WriteTransactionAsync(transaction);
            }
        }

        public async Task<IEnumerable<PurseDB>> GetPursesAsync()
        {
            await EnsureInitializedAsync();
            var files = Directory.GetFiles(_pursesDir, "*.json");
            var result = new List<PurseDB>(files.Length);
            foreach (var file in files)
            {
                var purse = await ReadPurseFromFileAsync(file);
                if (purse != null)
                    result.Add(purse);
            }
            return result;
        }

        public async Task<PurseDB?> GetPurseAsync(Guid id)
        {
            await EnsureInitializedAsync();
            var path = PursePath(id);
            if (!File.Exists(path))
                return null;
            return await ReadPurseFromFileAsync(path);
        }

        public async Task SavePurseAsync(PurseDB purse)
        {
            if (purse == null)
                throw new ArgumentNullException(nameof(purse));
            await EnsureInitializedAsync();
            await WritePurseAsync(purse);
        }

        public async Task DeletePurseAsync(Guid purseId)
        {
            await EnsureInitializedAsync();

            var path = PursePath(purseId);
            if (File.Exists(path))
                File.Delete(path);

            var transactionsDir = TransactionsDirectoryForPurse(purseId);
            if (Directory.Exists(transactionsDir))
                Directory.Delete(transactionsDir, recursive: true);
        }

        public async Task<IEnumerable<TransactionDB>> GetTransactionsByPurseIdAsync(Guid purseId)
        {
            await EnsureInitializedAsync();
            var dir = TransactionsDirectoryForPurse(purseId);
            if (!Directory.Exists(dir))
                return Enumerable.Empty<TransactionDB>();

            var files = Directory.GetFiles(dir, "*.json");
            var result = new List<TransactionDB>(files.Length);
            foreach (var file in files)
            {
                var transaction = await ReadTransactionFromFileAsync(file);
                if (transaction != null)
                    result.Add(transaction);
            }
            return result;
        }

        public async Task<TransactionDB?> GetTransactionByIdAsync(Guid transactionId)
        {
            await EnsureInitializedAsync();
            if (!Directory.Exists(_transactionsDir))
                return null;

            foreach (var purseDir in Directory.GetDirectories(_transactionsDir))
            {
                var path = Path.Combine(purseDir, transactionId + ".json");
                if (File.Exists(path))
                    return await ReadTransactionFromFileAsync(path);
            }
            return null;
        }

        public async Task SaveTransactionAsync(TransactionDB transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            await EnsureInitializedAsync();
            await WriteTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            await EnsureInitializedAsync();
            if (!Directory.Exists(_transactionsDir))
                return;

            foreach (var purseDir in Directory.GetDirectories(_transactionsDir))
            {
                var path = Path.Combine(purseDir, transactionId + ".json");
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return;
                }
            }
        }

        public async Task<decimal> BalanceByPurseIdAsync(Guid purseId)
        {
            await EnsureInitializedAsync();
            var purse = await GetPurseAsync(purseId)
                ?? throw new ArgumentException($"Purse with id {purseId} not found.", nameof(purseId));
            var transactions = await GetTransactionsByPurseIdAsync(purseId);
            return purse.StartBalance + transactions.Sum(t => t.Amount);
        }

        private string PursePath(Guid purseId) => Path.Combine(_pursesDir, purseId + ".json");

        private string TransactionsDirectoryForPurse(Guid purseId) => Path.Combine(_transactionsDir, purseId.ToString());

        private string TransactionPath(Guid purseId, Guid transactionId)
            => Path.Combine(TransactionsDirectoryForPurse(purseId), transactionId + ".json");

        private async Task WritePurseAsync(PurseDB purse)
        {
            var json = JsonSerializer.Serialize(purse, _jsonOptions);
            await File.WriteAllTextAsync(PursePath(purse.Id), json);
        }

        private async Task WriteTransactionAsync(TransactionDB transaction)
        {
            var dir = TransactionsDirectoryForPurse(transaction.PurseId);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var json = JsonSerializer.Serialize(transaction, _jsonOptions);
            await File.WriteAllTextAsync(TransactionPath(transaction.PurseId, transaction.Id), json);
        }

        private async Task<PurseDB?> ReadPurseFromFileAsync(string path)
        {
            try
            {
                await using var stream = File.OpenRead(path);
                return await JsonSerializer.DeserializeAsync<PurseDB>(stream, _jsonOptions);
            }
            catch
            {
                return null;
            }
        }

        private async Task<TransactionDB?> ReadTransactionFromFileAsync(string path)
        {
            try
            {
                await using var stream = File.OpenRead(path);
                return await JsonSerializer.DeserializeAsync<TransactionDB>(stream, _jsonOptions);
            }
            catch
            {
                return null;
            }
        }
    }
}
