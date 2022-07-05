using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class AddFeedChannelView : ContentPage
    {
        public AddFeedChannelView()
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();

            this.BindingContext = new AddFeedChannelViewModel(Navigation, userDialogs);
        }
    }
}
