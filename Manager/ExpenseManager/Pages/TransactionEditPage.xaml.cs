using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;

public partial class TransactionEditPage : ContentPage
{
    public TransactionEditPage(TransactionEditVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}