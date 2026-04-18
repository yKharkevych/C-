using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    // BaseViewModel provides common functionality for all view models
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public partial bool IsBusy { get; set; }

        public bool IsNotBusy => !IsBusy;

        protected async Task ExecuteBusyAsync(Func<Task> operation, string? errorTitle = null)
        {
            if (IsBusy)
                return; 

            IsBusy = true;
            try
            {
                await operation();
            }
            catch (ValidationException vex)
            {
                await ShowAlertAsync(errorTitle ?? "Invalid data", vex.Message);
            }
            catch (Exception ex)
            {
                await ShowAlertAsync(errorTitle ?? "Error", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static Task ShowAlertAsync(string title, string message)
        {
            var page = Shell.Current?.CurrentPage ?? Application.Current?.Windows[0]?.Page;
            if (page == null)
                return Task.CompletedTask;
            return page.DisplayAlert(title, message, "OK");
        }
    }
}
