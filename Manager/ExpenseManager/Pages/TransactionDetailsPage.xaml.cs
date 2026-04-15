using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;

public partial class TransactionDetailsPage : ContentPage
{
	
    public TransactionDetailsPage(TransactionDetailsVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}