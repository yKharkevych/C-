using Manager.ExpenseManager.UIModels;

namespace Manager.ExpenseManager.Pages;

[QueryProperty(nameof(CurrentTransaction), nameof(CurrentTransaction))]
public partial class TransactionDetailsPage : ContentPage
{
	private TransactionUI _currentTransaction;

	// створюємо властивість для поточної транзакції, яка буде використовуватися для передачі даних між сторінками
	public TransactionUI CurrentTransaction
	{
		get => _currentTransaction;
		set 
		{
			_currentTransaction = value;
			BindingContext = CurrentTransaction;
        }
	}
    public TransactionDetailsPage()
	{
		InitializeComponent();
	}
}