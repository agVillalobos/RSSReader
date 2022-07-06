using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Models;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class RSSFeedDetailView : ContentPage
    {
        public RSSFeedDetailView(FeedItem feedItem)
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();
            var feedService = DependencyService.Get<IFeedService>();

            this.BindingContext = new RSSFeedDetailViewModel(Navigation, userDialogs, feedService, feedItem);
        }
    }
}
