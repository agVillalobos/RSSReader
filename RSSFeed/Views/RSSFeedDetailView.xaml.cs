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
        public bool IsOn { get; set; }
        private bool initAnimation;

        public RSSFeedDetailView(FeedItem feedItem)
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();
            var feedService = DependencyService.Get<IFeedService>();

            IsOn = feedItem.Favorite;
            initAnimation = true;

            this.BindingContext = new RSSFeedDetailViewModel(Navigation, userDialogs, feedService, feedItem);
        }

        public void Animate()
        {
            if (!animationView.IsAnimating)
            {
                if (IsOn)
                {
                    animationView.PlayMinAndMaxFrame(1, 2);
                }
                else
                {
                    animationView.PlayMinAndMaxFrame(1, 60);
                }

                animationView.Progress = 0;
                IsOn = !IsOn;
            }
        }

        private void Clicked(object sender, EventArgs e)
        {
            Animate();
        }

        private void Finished(object sender, EventArgs e)
        {
            if (initAnimation)
            {
                if (!IsOn)
                {
                    animationView.PlayMinAndMaxFrame(1, 2);
                }
                initAnimation = false;
            }
            animationView.AutoPlay = false;
        }
    }
}
