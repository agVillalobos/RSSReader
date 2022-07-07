using System;
using System.Collections.Generic;
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
    public class FeedChannelViewModel: BaseViewModel
    {
        private readonly IFeedService feedService;
        private ObservableCollection<FeedChannel> feedChannelList;
        private FeedChannel selectedFeedChannelItem;
        private string searchText;

        public FeedChannelViewModel(
            INavigation navigation,
            IFeedService feedService,
            IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            this.feedService = feedService;

            FeedChannelList = new ObservableCollection<FeedChannel>();

            AddFeedChannelCommand = new Command(async () => await NavigateToAddFeedChannel());
            FeedChannelItemSelectedCommand = new Command(async () => await SeletFeedChannel());
            SearchCommand = new Command<string>(SearchChannelName);
            RefreshCommand.Execute(this);

            MessagingCenter.Subscribe<AddFeedChannelViewModel, string>(
              this,
              "AddFeedChannel",
              async (sender, args) => await AddFeedChannel(args));
        }

        public ICommand AddFeedChannelCommand { get; private set; }
        public ICommand FeedChannelItemSelectedCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        private void SearchChannelName() => SearchChannelName(SearchText);

        private void SearchChannelName(string channelName)
        {
            IsRefreshing = true;

            if (string.IsNullOrEmpty(channelName))
            {
                RefreshCommand.Execute(this);
            }
            else
            {
                FeedChannelList.Clear();
                var chhannels = feedService.FeedChannels;
                foreach (var channel in chhannels.Where(channel => channel.Title.Contains(channelName)).ToList())
                {
                    FeedChannelList.Add(channel);
                }
            }

            IsRefreshing = false;
        }

        private async Task SeletFeedChannel()
        {
            await NavigationService.PushModalAsync(new RSSFeedView(SelectedFeedChannelItem));
        }

        public override async Task Refresh()
        {
            try
            {
                IsRefreshing = true;

                FeedChannelList = new ObservableCollection<FeedChannel>(feedService.FeedChannels);

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);

            } finally
            {
                IsRefreshing = false;
            }

            await base.Refresh();
        }

        private async Task AddFeedChannel(string url)
        {
            var feedChannel = await feedService.GetFeedChannelAsync(url);
            var result = await feedService.AddFeedChannelAsync(url);
            await Task.Delay(1000);

            if (!result)
            {
                //Display error to user.
                UserDialogs.Toast("Error to add RSS Feed, incorrect URL", TimeSpan.FromSeconds(4));
            }
            else
            {
                FeedChannelList.Add(feedChannel);
                UserDialogs.Toast("Added correctly", TimeSpan.FromSeconds(4));
            }
        }

        private async Task NavigateToAddFeedChannel()
        {
            await NavigationService.PushModalAsync(new AddFeedChannelView());
        }

        public FeedChannel SelectedFeedChannelItem
        {
            get => selectedFeedChannelItem;
            set => SetProperty(ref selectedFeedChannelItem, value);
        }

        public ObservableCollection<FeedChannel> FeedChannelList
        {
            get => feedChannelList;
            set => SetProperty(ref feedChannelList, value);
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                SearchChannelName();
            }
        }
    }
}
