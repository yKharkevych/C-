using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.ViewModel;
namespace Manager.ExpenseManager.Pages;

public partial class TransactionCreatePage : ContentPage
{
    public TransactionCreatePage(TransactionCreateVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}