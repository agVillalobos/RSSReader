using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Models;
using RSSFeed.Views;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class RSSFeedViewModel: BaseViewModel
    {
        private ObservableCollection<FeedItem> feedItemList;
        private FeedItem selectedItem;
        private string searchText;
        private readonly FeedChannel feedChannel;

        public RSSFeedViewModel(INavigation navigation, IUserDialogs userDialogs, FeedChannel feedChannel): base(navigation, userDialogs)
        {
            this.feedChannel = feedChannel;

            FeedItemSelectedCommand = new Command(async () => await SelectFeedItem());
            SearchCommand = new Command<string>(SearchTitleName);

            RefreshCommand.Execute(this);
        }

        public ICommand FeedItemSelectedCommand { get; protected set; }
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
                var items = feedChannel.Items;
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
                Title = feedChannel.Title;
                FeedItemList = new ObservableCollection<FeedItem>(feedChannel.Items);
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
            await NavigationService.PushAsync(new RSSFeedDetailView(selectedItem));
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
