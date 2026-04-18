using Manager.ExpenseManager.ViewModel;

namespace Manager.ExpenseManager.Pages;

public partial class PurseCreatePage : ContentPage
{
    public PurseCreatePage(PurseCreateVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}