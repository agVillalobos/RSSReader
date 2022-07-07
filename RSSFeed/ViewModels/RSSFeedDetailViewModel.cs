using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Models;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class RSSFeedDetailViewModel : BaseViewModel
    {
        private FeedItem feedItem;
        private bool isAnimating;
        private readonly IFeedService feedService;

        public RSSFeedDetailViewModel(
            INavigation navigation,
            IUserDialogs userDialogs,
            IFeedService feedService,
            FeedItem feedItem) : base(navigation, userDialogs)
        {
            FeedItem = feedItem;
            this.feedService = feedService;

            OpenWebViewCommand = new Command(async () => await OpenWebView());
            AddRemoveMyFavoritesCommand = new Command(AddRemoveMyFavorites);
        }

        private void AddRemoveMyFavorites()
        {
            feedItem.Favorite = !feedItem.Favorite;
            String message = null;
            if (feedItem.Favorite)
            {
                var isFeedItemAddedSuccessfully = feedService.AddFeedItem(feedItem);
                message = isFeedItemAddedSuccessfully ? "Article added correctly to my favorites" : "You already added this article";
            } else
            {
                var isFeedItemRemmovedSuccessfully = feedService.RemoveFeedItem(feedItem);
                message = isFeedItemRemmovedSuccessfully ? "Article was removed correctly to my favorites" : "No articles to remove";
            }

            UserDialogs.Toast(message, TimeSpan.FromSeconds(2));

        }

        private async Task OpenWebView()
        {
            await NavigationService.PushModalAsync(new Views.WebPage(FeedItem.Guid));
        }

        protected override async Task Close()
        {
            await NavigationService.PopModalAsync();
        }

        public ICommand OpenWebViewCommand { get; private set; }
        public ICommand AddRemoveMyFavoritesCommand { get; private set; }

        public bool IsAnimating
        {
            get => isAnimating;
            set => SetProperty(ref isAnimating, value);
        }

        public FeedItem FeedItem
        {
            get => feedItem;
            set => SetProperty(ref feedItem, value);
        }

    }
}
