using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;

public partial class PurseEditPage : ContentPage
{
    public PurseEditPage(PurseEditVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}