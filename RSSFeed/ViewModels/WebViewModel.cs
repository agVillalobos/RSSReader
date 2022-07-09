using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class WebViewModel : BaseViewModel
    {
        private string webviewSource;

        public WebViewModel(INavigation navigation,IUserDialogs userDialogs, string webURL): base(navigation, userDialogs)
        {
            WebviewSource = webURL;
        }

        protected override async Task Close() => await NavigationService.PopModalAsync();
        
        public string WebviewSource
        {
            get => webviewSource;
            set => SetProperty(ref webviewSource, value);
        }
    }
}
