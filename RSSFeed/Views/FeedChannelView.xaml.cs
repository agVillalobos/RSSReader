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

            this.BindingContext = new FeedChannelViewModel(Navigation, feedService, userDialogs);
        }

        void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
        }
    }
}
