using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    public partial class PurseCreateVM : BaseViewModel
    {
        private readonly IPurseService _purseService;

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;
        public EnumWithName<Currency>[] Currencies { get; } = EnumExtensions.GetValuesWithNames<Currency>();

        [ObservableProperty]
        public partial EnumWithName<Currency>? SelectedCurrency { get; set; }

        [ObservableProperty]
        public partial decimal StartBalance { get; set; }

        public PurseCreateVM(IPurseService purseService)
        {
            _purseService = purseService;
            SelectedCurrency = Currencies.Length > 0 ? Currencies[0] : null;
        }

        [RelayCommand]
        private Task CreateAsync() => ExecuteBusyAsync(async () =>
        {
            if (SelectedCurrency == null)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException("Please select a currency.");
            }

            var dto = new PurseCreateDTO(Name, SelectedCurrency.Value, StartBalance);
            await _purseService.CreatePurseAsync(dto);

            await Shell.Current.GoToAsync("..");
        });

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}
