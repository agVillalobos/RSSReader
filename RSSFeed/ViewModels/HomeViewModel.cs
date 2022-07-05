using System;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel(INavigation navigation, IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
        }
    }
}
