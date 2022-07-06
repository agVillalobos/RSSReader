using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class MyFavoritesView : ContentPage
    {
        private MyFavoritesViewModel myFavoritesViewModel;

        public MyFavoritesView()
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();
            var feedService = DependencyService.Get<IFeedService>();

            myFavoritesViewModel = new MyFavoritesViewModel(Navigation, userDialogs, feedService);
            BindingContext = myFavoritesViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            myFavoritesViewModel.OnAppearingAsync();
        }
    }
}
