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
        private bool isOn;

        public RSSFeedDetailView(FeedItem feedItem)
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();
            var feedService = DependencyService.Get<IFeedService>();

            isOn = feedItem.Favorite;
            animationView.AutoPlay = feedItem.Favorite;

            this.BindingContext = new RSSFeedDetailViewModel(Navigation, userDialogs, feedService, feedItem);
        }

        private void Clicked(object sender, EventArgs e)
        {
            if (!animationView.IsAnimating)
            {
                if (isOn) { animationView.PlayMinAndMaxFrame(1, 2); }
                else { animationView.PlayMinAndMaxFrame(1, 60); }

                animationView.Progress = 0;
                isOn = !isOn;
            }
        }

        private void Finished(object sender, EventArgs e)
        {
            animationView.AutoPlay = false;
        }
    }
}
