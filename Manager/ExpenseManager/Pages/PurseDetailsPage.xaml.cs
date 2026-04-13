using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.UIModels;

namespace Manager.ExpenseManager.Pages;

[QueryProperty(nameof(CurrentPurse), nameof(CurrentPurse))]

public partial class PurseDetailsPage : ContentPage
{
    private IStorageService _storage;
    private PurseUI _currentPurse;

    // створюємо властивість для поточного гаманця, яка буде використовуватися для передачі даних між сторінками
    public PurseUI CurrentPurse { 
		get => _currentPurse;
		set
		{
			_currentPurse = value;
			_currentPurse.LoadTransactions();
            BindingContext = CurrentPurse;
        } 
	}

    public PurseDetailsPage(IStorageService storage)
	{
		InitializeComponent();
        _storage = storage;
    }

    //Метод для обробки вибору транзакції зі списку. При виборі транзакції, користувач буде перенаправлений на сторінку з деталями цієї транзакції.
    private void TransactionSelected(object sender, SelectionChangedEventArgs e)
    {
        var transaction = (TransactionUI)e.CurrentSelection[0];
        Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}", new Dictionary<string, object> { { nameof(TransactionDetailsPage.CurrentTransaction), transaction } });
    }
}

