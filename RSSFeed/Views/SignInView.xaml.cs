using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();

            var userService = DependencyService.Get<IUserService>();
            var userDialogs = DependencyService.Get<IUserDialogs>();

            this.BindingContext = new SignInViewModel(Navigation, userService, userDialogs);
        }
    }
}
