using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DTOModels.Purses;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    public partial class PurseEditVM : BaseViewModel, IQueryAttributable
    {
        private readonly IPurseService _purseService;

        private Guid _purseId;

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        public PurseEditVM(IPurseService purseService)
        {
            _purseService = purseService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("PurseId", out var purseIdObj) && purseIdObj is Guid purseId)
            {
                _purseId = purseId;
            }
            _ = LoadAsync();
        }

        [RelayCommand]
        private Task LoadAsync() => ExecuteBusyAsync(async () =>
        {
            if (_purseId == Guid.Empty)
                return;
            var dto = await _purseService.GetPurseForEditAsync(_purseId);
            if (dto != null)
            {
                Name = dto.Name;
            }
        });

        [RelayCommand]
        private Task SaveAsync() => ExecuteBusyAsync(async () =>
        {
            var dto = new PurseEditDTO(_purseId, Name);
            await _purseService.UpdatePurseAsync(dto);
            await Shell.Current.GoToAsync("..");
        });

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}
