using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class WebPage : ContentPage
    {

        public WebPage(string webViewSource)
        {
            InitializeComponent();
            Title = "Web";
            var userDialogs = DependencyService.Get<IUserDialogs>();
            BindingContext = new WebViewModel(Navigation, userDialogs, webViewSource) ;
        }
    }
}
