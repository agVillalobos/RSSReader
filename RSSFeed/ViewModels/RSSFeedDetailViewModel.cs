using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Models;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class RSSFeedDetailViewModel : BaseViewModel
    {
        private FeedItem feedItem;

        public RSSFeedDetailViewModel(
            INavigation navigation,
            IUserDialogs userDialogs,
            FeedItem feedItem) : base(navigation, userDialogs)
        {
            FeedItem = feedItem;

            OpenWebViewCommand = new Command(async () => await OpenWebView());
        }

        private async Task OpenWebView()
        {
            await NavigationService.PushAsync(new Views.WebPage(FeedItem.Guid));
        }

        public ICommand OpenWebViewCommand { get; set; }

        public FeedItem FeedItem
        {
            get => feedItem;
            set => SetProperty(ref feedItem, value);
        }
    }
}
