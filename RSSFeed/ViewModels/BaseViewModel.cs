using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        private bool isRefreshing;
        private string title;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel(INavigation navigation, IUserDialogs userDialogs)
        {
            NavigationService = navigation;
            UserDialogs = userDialogs;
            CloseCommand = new Command(async () => await Close());
            RefreshCommand = new Command(async () => await Refresh());
        }

        public INavigation NavigationService { get; private set; }
        public IUserDialogs UserDialogs { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty<bool>(ref isRefreshing, value, nameof(IsRefreshing));
        }

        public bool SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;

            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        public virtual Task OnAppearingAsync()=> Task.FromResult(true);
        
        public virtual Task OnDisappearingAsync() => Task.FromResult(true);

        public virtual Task Refresh() => Task.FromResult(true);

        protected virtual async Task Close() =>  await NavigationService.PopAsync();
    }
}
