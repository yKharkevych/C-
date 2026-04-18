using Manager.ExpenseManager.Pages;

namespace Manager.ExpenseManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Реєструємо маршрути для навігації між сторінками
            Routing.RegisterRoute(nameof(PurseDetailsPage), typeof(PurseDetailsPage));
            Routing.RegisterRoute(nameof(PurseCreatePage), typeof(PurseCreatePage));
            Routing.RegisterRoute(nameof(PurseEditPage), typeof(PurseEditPage));
            Routing.RegisterRoute(nameof(TransactionDetailsPage), typeof(TransactionDetailsPage));
            Routing.RegisterRoute(nameof(TransactionCreatePage), typeof(TransactionCreatePage));
            Routing.RegisterRoute(nameof(TransactionEditPage), typeof(TransactionEditPage));
        }
    }
}
