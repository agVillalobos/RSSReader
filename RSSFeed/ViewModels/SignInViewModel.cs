using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Views;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class SignInViewModel: BaseViewModel
    {
        private readonly IUserService userService;
        private string userName;
        private string password;
        private string imageURL;

        public SignInViewModel(
            INavigation navigation,
            IUserService userService,
            IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            this.userService = userService;

            //Fake values
            UserName = "Test";
            Password = "1234";
            ImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSwjP5j6-YaTf99Z-w2wExdjc2HPfJ7jBuDIA&usqp=CAU";

            SignInCommand = new Command(async () => await SignIn());
            SignUpCommand = new Command(async () => await SignUp());
        }

        public ICommand SignInCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }


        public string ImageURL
        {
            get => imageURL;
            set => SetProperty(ref imageURL, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private async Task SignIn()
        {
            var cancelSrc = new CancellationTokenSource();
            var config = new ProgressDialogConfig()
                .SetTitle("Loading")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.None)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (UserDialogs.Progress(config))
            {
                try
                {
                    var logginSuccesful = await this.userService.SignIn(UserName, Password);

                    await Task.Delay(TimeSpan.FromSeconds(2), cancelSrc.Token);

                    if (logginSuccesful)
                    {
                        App.Current.MainPage = new HomeView();
                    }
                    else
                    {
                        UserDialogs.Alert("Incorrect user name or password", "Error");
                    }
                } catch { }
            }
        }

        private async Task SignUp() => await NavigationService.PushAsync(new SignUpView());
    }
}
