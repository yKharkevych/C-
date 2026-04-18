using Manager.ExpenseManager.Pages;
using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.Storage;
using Manager.ExpenseManager.Repositories;
using Microsoft.Extensions.Logging;
using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IStorageContext>(
                sp => new FileStorageContext(FileSystem.Current.AppDataDirectory));

            builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
            builder.Services.AddSingleton<IPurseRepository, PurseRepository>();

            // Registering services for handling business logic related to purses and transactions
            builder.Services.AddSingleton<IPurseService, PurseService>();
            builder.Services.AddSingleton<ITransactionService, TransactionService>();

            // Registering pages and their corresponding view models for navigation and data binding
            builder.Services.AddSingleton<PursesPage>();
            builder.Services.AddTransient<PurseDetailsPage>();
            builder.Services.AddTransient<PurseCreatePage>();
            builder.Services.AddTransient<PurseEditPage>();
            builder.Services.AddTransient<TransactionDetailsPage>();
            builder.Services.AddTransient<TransactionCreatePage>();
            builder.Services.AddTransient<TransactionEditPage>();

            // Registering view models for the pages, with appropriate lifetimes 
            builder.Services.AddSingleton<PursesVM>();
            builder.Services.AddTransient<PurseDetailsVM>();
            builder.Services.AddTransient<PurseCreateVM>();
            builder.Services.AddTransient<PurseEditVM>();
            builder.Services.AddTransient<TransactionDetailsVM>();
            builder.Services.AddTransient<TransactionCreateVM>();
            builder.Services.AddTransient<TransactionEditVM>();

            return builder.Build();
        }
    }
}
