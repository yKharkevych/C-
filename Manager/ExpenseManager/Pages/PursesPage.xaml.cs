using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.ViewModel;
using System.Collections.ObjectModel;

namespace Manager.ExpenseManager.Pages;

public partial class PursesPage : ContentPage
{
    private readonly PursesVM _vm;
    public PursesPage(PursesVM vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadPursesCommand.ExecuteAsync(null);
    }
}