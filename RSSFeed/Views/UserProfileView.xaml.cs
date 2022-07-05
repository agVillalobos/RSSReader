using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class UserProfileView : ContentPage
    {
        public UserProfileView()
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();

            this.BindingContext = new UserProfileViewModel(Navigation, userDialogs);
        }
    }
}
