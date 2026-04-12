using Manager.ExpenseManager.Pages;
using Manager.ExpenseManager.Services;
using Microsoft.Extensions.Logging;

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
            builder.Services.AddSingleton<IStorageService, StorageService>();

            builder.Services.AddSingleton<PursesPage>();
            builder.Services.AddTransient<PurseDetailsPage>();
            builder.Services.AddTransient<TransactionDetailsPage>();

            return builder.Build();
        }
    }
}
