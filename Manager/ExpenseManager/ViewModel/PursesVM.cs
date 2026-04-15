using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Pages;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    // ViewModel for the PursesPage, responsible for handling the logic and data for displaying the list of purses and navigating to purse details.
    public partial class PursesVM : ObservableObject
    {
        private readonly IPurseService _purseService;

        [ObservableProperty]
        public partial ObservableCollection<PurseListDTO> Purses { get; set; }

        [ObservableProperty]
        public partial PurseListDTO? SelectedPurse { get; set; }

        public PursesVM(IPurseService purseService)
        {
            _purseService = purseService;
            Purses = new ObservableCollection<PurseListDTO>(_purseService.GetPurses());
        }

       
        [RelayCommand]
        private async Task PurseSelected() 
        {
            if (SelectedPurse == null)
                return;
            var purseId = SelectedPurse.Id;
            SelectedPurse = null;
            await Shell.Current.GoToAsync($"{nameof(PurseDetailsPage)}", new Dictionary<string, object>
            {
                { "PurseId", purseId }
            });
        }
    }
}
