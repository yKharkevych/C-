using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;
public partial class PurseDetailsPage : ContentPage
{
    private readonly PurseDetailsVM _vm;
    public PurseDetailsPage(PurseDetailsVM vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadCommand.ExecuteAsync(null);
    }
}

