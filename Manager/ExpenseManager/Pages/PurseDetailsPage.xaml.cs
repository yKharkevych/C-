using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;
public partial class PurseDetailsPage : ContentPage
{
    public PurseDetailsPage(PurseDetailsVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}

