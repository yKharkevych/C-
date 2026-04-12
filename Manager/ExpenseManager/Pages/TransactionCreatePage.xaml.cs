using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.UIModels;
namespace Manager.ExpenseManager.Pages;

public partial class TransactionCreatePage : ContentPage
{
	public TransactionCreatePage()
	{
		InitializeComponent();

		pCategory.ItemsSource = EnumExtensions.GetValuesWithNames<Category>();
	}

    //метод обробки кнопки створення транзакції. Перевіряє правильність введених даних, створює об'єкт TransactionUI та відображає повідомлення про успішне створення транзакції.
    private void CreateClicked(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(eDescription.Text))
        {
			eDescription.Text = "";
        }

        if (String.IsNullOrWhiteSpace(eAmount.Text) ||
        !decimal.TryParse(eAmount.Text, out decimal amount) ||
        decimal.Round(amount, 2) != amount)
		{
			DisplayAlert("Incorrect data!", "Invalid or null amount. Please enter a number with maximum 2 decimal places.", "OK");
            return;
		}

        if (pCategory.SelectedItem == null)
        {
            DisplayAlert("Incomplete data!", "Category must be selected", "OK");
            return;
        }

        if (dDate.Date == null)
        {
            DisplayAlert("Incomplete data!", "Date must be selected", "OK");
            return;
        }

        var transaction = new TransactionUI(Guid.Empty);
        transaction.Amount = amount;
        transaction.Description = eDescription.Text;
        transaction.Category = ((EnumWithName<Category>)pCategory.SelectedItem).Value;
        transaction.Date = dDate.Date.Value;

        DisplayAlert("Transaction created!", $"Transaction was successfully created.", "OK");
    }

    //метод обробки кнопки повернення назад
    private void BackClicked(object sender, EventArgs e)
    {

    }
}