using System;
using System.Collections.Generic;
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
    public class FeedChannelViewModel: BaseViewModel
    {
        private readonly IFeedService feedService;
        private ObservableCollection<FeedChannel> feedChannelList;
        private FeedChannel selectedFeedChannelItem;
        
        public FeedChannelViewModel(
            INavigation navigation,
            IFeedService feedService,
            IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            this.feedService = feedService;

            FeedChannelList = new ObservableCollection<FeedChannel>();

            AddFeedChannelCommand = new Command(async () => await NavigateToAddFeedChannel());
            FeedChannelItemSelectedCommand = new Command(async () => await SeletFeedChannel());
            RefreshCommand.Execute(this);

            MessagingCenter.Subscribe<AddFeedChannelViewModel, string>(
              this,
              "AddFeedChannel",
              async (sender, args) => await AddFeedChannel(args));
        }

        public ICommand AddFeedChannelCommand { get; private set; }
        public ICommand FeedChannelItemSelectedCommand { get; private set; }

        private async Task SeletFeedChannel()
        {
            await NavigationService.PushAsync(new RSSFeedView(SelectedFeedChannelItem));
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

            if (!result)
            {
                //Display error to user.
            }
            else
            {
                FeedChannelList.Add(feedChannel);
            }
        }

        private async Task NavigateToAddFeedChannel()
        {
            await NavigationService.PushAsync(new AddFeedChannelView());
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
    }
}
