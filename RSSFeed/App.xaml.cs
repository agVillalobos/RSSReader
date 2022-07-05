using System;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Services;
using RSSFeed.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSSFeed
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton<IFeedService>(new FeedService());
            DependencyService.RegisterSingleton<IUserService>(new UserService());
            DependencyService.RegisterSingleton<IUserDialogs>(UserDialogs.Instance);

            MainPage = new NavigationPage(new SignInView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
