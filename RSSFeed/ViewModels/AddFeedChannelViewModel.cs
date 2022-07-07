using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class AddFeedChannelViewModel: BaseViewModel
    {
        private string feedUrl;

        public AddFeedChannelViewModel(INavigation navigation, IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            FeedUrl = "https://";

            AddFeedChannelCommand = new Command(async () => await AddFeedChannel());
        }

        public ICommand AddFeedChannelCommand { get; private set; }

        public string FeedUrl
        {
            get => feedUrl;
            set => SetProperty(ref feedUrl, value);
        }

        private async Task AddFeedChannel()
        {
            MessagingCenter.Send<AddFeedChannelViewModel, string>(this, "AddFeedChannel", FeedUrl);
            await NavigationService.PopModalAsync();
        }

        protected override async Task Close()
        {
            await NavigationService.PopModalAsync();
        }

    }
}
