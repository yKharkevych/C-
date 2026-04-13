using Manager.ExpenseManager.Pages;

namespace Manager.ExpenseManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Реєструємо маршрути для навігації між сторінками
            Routing.RegisterRoute("PursesPage/PurseDetailsPage", typeof(PurseDetailsPage));
            Routing.RegisterRoute("PursesPage/PurseDetailsPage/TransactionDetailsPage", typeof(TransactionDetailsPage));
        }
    }
}
