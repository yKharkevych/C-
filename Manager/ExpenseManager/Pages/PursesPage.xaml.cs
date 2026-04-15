using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.ViewModel;
using System.Collections.ObjectModel;

namespace Manager.ExpenseManager.Pages;

public partial class PursesPage : ContentPage
{
    public PursesPage(PursesVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}