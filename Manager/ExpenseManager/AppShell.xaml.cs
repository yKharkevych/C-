using Manager.ExpenseManager.Pages;

namespace Manager.ExpenseManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("PursesPage/PurseDetailsPage", typeof(PurseDetailsPage));
            Routing.RegisterRoute("PursesPage/PurseDetailsPage/TransactionDetailsPage", typeof(TransactionDetailsPage));
        }
    }
}
