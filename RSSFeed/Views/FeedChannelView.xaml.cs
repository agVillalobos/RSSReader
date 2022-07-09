using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class FeedChannelView : ContentPage
    {
        public FeedChannelView()
        {
            InitializeComponent();

            var feedService = DependencyService.Get<IFeedService>();
            var userDialogs = DependencyService.Get<IUserDialogs>();
            var vibrationService = DependencyService.Get<IVibrationService>();

            this.BindingContext = new FeedChannelViewModel(Navigation, feedService, userDialogs, vibrationService);
        }
    }
}
