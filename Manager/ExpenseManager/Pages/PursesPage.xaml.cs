using Manager.ExpenseManager.Services;
using Manager.ExpenseManager.UIModels;
using System.Collections.ObjectModel;

namespace Manager.ExpenseManager.Pages;

public partial class PursesPage : ContentPage
{
	private IStorageService _storage;

    //Клас для відображення гаманців
    public ObservableCollection<PurseUI> Purses { get; set; } 

    public PursesPage(IStorageService storageService)
	{
		InitializeComponent();
        _storage = storageService;
        Purses = new ObservableCollection<PurseUI>();
        foreach (var purse in _storage.GetAllPurses())
        {
            Purses.Add(new PurseUI(_storage, purse));
        }
        BindingContext = this;
    }

    //Метод для переходу на сторінку деталей гаманця при виборі його зі списку
    private async void PurseSelected(object sender, SelectionChangedEventArgs e)
    {
        var purse = (PurseUI)e.CurrentSelection[0];
        await Shell.Current.GoToAsync($"{nameof(PurseDetailsPage)}", new Dictionary<string, object> { { nameof(PurseDetailsPage.CurrentPurse), purse } });
    }
}