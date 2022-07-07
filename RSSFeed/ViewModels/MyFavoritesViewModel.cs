using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        private string searchText;

        public MyFavoritesViewModel(
            INavigation navigation,
            IUserDialogs userDialogs,
            IFeedService feedService) : base(navigation, userDialogs)
        {
            this.feedService = feedService;

            FeedItemSelectedCommand = new Command(async () => await SelectFeedItem());
            RemoveFeedItemCommand = new Command<FeedItem>(RemoveFeedItem);
            SearchCommand = new Command<string>(SearchTitleName);

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
        public ICommand SearchCommand { get; private set; }

        private void SearchTitleName(string titleName)
        {
            if (string.IsNullOrEmpty(titleName))
            {
                RefreshCommand.Execute(this);
            }
            else
            {
                FeedItemList.Clear();
                var items = feedService.MyFavoriteItems;
                var titleNameLower = titleName.ToLower();
                foreach (var channel in items.Where(item => item.Title.ToLower().Contains(titleNameLower)))
                {
                    FeedItemList.Add(channel);
                }
            }
        }

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

        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                SearchTitleName(SearchText);
            }
        }
    }
}
