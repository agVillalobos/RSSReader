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
            AddMyFavoritesCommand = new Command(AddMyFavorites);
        }

        private void AddMyFavorites()
        {
            var isFeedItemAddedSuccessfully = feedService.AddFeedItem(feedItem);

            var message = isFeedItemAddedSuccessfully ? "Article added correctly to my favorites" : "You already added this article";

            UserDialogs.Toast(message, TimeSpan.FromSeconds(2));
        }

        private async Task OpenWebView()
        {
            await NavigationService.PushAsync(new Views.WebPage(FeedItem.Guid));
        }

        public ICommand OpenWebViewCommand { get; private set; }

        public ICommand AddMyFavoritesCommand { get; private set; }

        public FeedItem FeedItem
        {
            get => feedItem;
            set => SetProperty(ref feedItem, value);
        }

    }
}
