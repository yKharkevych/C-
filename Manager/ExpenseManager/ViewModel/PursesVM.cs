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
    public partial class PursesVM : BaseViewModel
    {
        private readonly IPurseService _purseService;

        private List<PurseListDTO> _allPurses = new();

        [ObservableProperty]
        public partial ObservableCollection<PurseListDTO> Purses { get; set; } = new();

        [ObservableProperty]
        public partial PurseListDTO? SelectedPurse { get; set; }

        [ObservableProperty]
        public partial string SearchText { get; set; } = string.Empty;

        public IReadOnlyList<string> SortOptions { get; } = new[]
        {
            "Name (A-Z)",
            "Name (Z-A)",
            "Balance (high to low)",
            "Balance (low to high)",
        };

        [ObservableProperty]
        public partial string SelectedSortOption { get; set; } = "Name (A-Z)";

        partial void OnSearchTextChanged(string value) => ApplyFilterAndSort();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilterAndSort();

        public PursesVM(IPurseService purseService)
        {
            _purseService = purseService;
        }

        
        [RelayCommand]
        private Task LoadPursesAsync() => ExecuteBusyAsync(async () =>
        {
            var purses = await _purseService.GetPursesAsync();
            _allPurses = purses.ToList();
            ApplyFilterAndSort();
        });

        private void ApplyFilterAndSort()
        {
            IEnumerable<PurseListDTO> result = _allPurses;
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var query = SearchText.Trim();
                result = result.Where(p =>
                    p.Name != null &&
                    p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            result = SelectedSortOption switch
            {
                "Name (A-Z)" => result.OrderBy(p => p.Name),
                "Name (Z-A)" => result.OrderByDescending(p => p.Name),
                "Balance (high to low)" => result.OrderByDescending(p => p.Balance),
                "Balance (low to high)" => result.OrderBy(p => p.Balance),
                _ => result
            };

            Purses = new ObservableCollection<PurseListDTO>(result);
        }

        [RelayCommand]
        private async Task PurseSelectedAsync()
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

        [RelayCommand]
        private Task AddPurseAsync()
            => Shell.Current.GoToAsync(nameof(PurseCreatePage));

        [RelayCommand]
        private Task DeletePurseAsync(Guid purseId) => ExecuteBusyAsync(async () =>
        {
            var page = Shell.Current?.CurrentPage;
            if (page == null) return;

            bool confirmed = await page.DisplayAlertAsync(
                "Delete purse",
                "Are you sure? All transactions of this purse will also be deleted.",
                "Delete", "Cancel");

            if (!confirmed)
                return;

            await _purseService.DeletePurseAsync(purseId);
            await LoadPursesAsync();
        });
    }
}
