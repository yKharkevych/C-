using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;

public partial class TransactionDetailsPage : ContentPage
{
    private readonly TransactionDetailsVM _vm;
    public TransactionDetailsPage(TransactionDetailsVM vm)
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