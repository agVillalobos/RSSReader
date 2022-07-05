using System;
using System.Collections.ObjectModel;
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
        private readonly FeedChannel feedChannel;

        public RSSFeedViewModel(INavigation navigation, IUserDialogs userDialogs, FeedChannel feedChannel): base(navigation, userDialogs)
        {
            this.feedChannel = feedChannel;

            FeedItemSelectedCommand = new Command(async () => await SelectFeedItem());

            RefreshCommand.Execute(this);
        }

        public ICommand FeedItemSelectedCommand { get; protected set; }

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
    }
}
