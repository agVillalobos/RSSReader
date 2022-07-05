using Acr.UserDialogs;
using RSSFeed.Models;
using RSSFeed.ViewModels;
using Xamarin.Forms;

namespace RSSFeed.Views
{
    public partial class RSSFeedView : ContentPage
    {
        public RSSFeedView(FeedChannel feedChannel)
        {
            InitializeComponent();

            var userDialogs = DependencyService.Get<IUserDialogs>();

            BindingContext = new RSSFeedViewModel(Navigation, userDialogs, feedChannel);
        }
    }
}
