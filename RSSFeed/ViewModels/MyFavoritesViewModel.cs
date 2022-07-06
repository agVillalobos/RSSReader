using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Models;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class MyFavoritesViewModel : BaseViewModel
    {
        private ObservableCollection<FeedItem> feedItemList;
        private FeedItem selectedItem;
        private readonly IFeedService feedService;

        public MyFavoritesViewModel(
            INavigation navigation,
            IUserDialogs userDialogs,
            IFeedService feedService) : base(navigation, userDialogs)
        {
            this.feedService = feedService;

            FeedItemSelectedCommand = new Command(async () => await SelectFeedItem());
            RemoveFeedItemCommand = new Command<FeedItem>(RemoveFeedItem);
            RefreshCommand.Execute(this);
        }

        private void RemoveFeedItem(FeedItem feedItem)
        {
            feedService.RemoveFeedItem(feedItem);
            FeedItemList.Remove(feedItem);
        }

        public override Task OnAppearingAsync()
        {
            RefreshCommand.Execute(this);
            return base.OnAppearingAsync();
        }

        public ICommand FeedItemSelectedCommand { get; protected set; }
        public ICommand RemoveFeedItemCommand { get; protected set; }

        public override async Task Refresh()
        {
            await base.Refresh();

            try
            {
                IsRefreshing = true;
                FeedItemList = new ObservableCollection<FeedItem>(feedService.MyFavoriteItems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsRefreshing = false;
            }

        }

        private async Task SelectFeedItem()
        {
            Console.WriteLine("Feed detail selected");
            //await NavigationService.PushAsync(new RSSFeedDetailView(selectedItem));
        }

        public ObservableCollection<FeedItem> FeedItemList
        {
            get => feedItemList;
            set => SetProperty(ref feedItemList, value);
        }

        public FeedItem SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
    }
}
