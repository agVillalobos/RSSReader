using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class SignUpViewModel: BaseViewModel
    {
        private readonly IUserService userService;

        public SignUpViewModel(INavigation navigation, IUserService userService, IUserDialogs userDialogs) : base(navigation, userDialogs)
        {

            this.userService = userService;

            SignUpCommand = new Command(async () => await SingUp());
        }

        private async Task SingUp()
        {
            var isSignUpSuccesfull = await userService.SignUp(new Models.User(Username, Password, Email, Surname, Lastname, "", Phone));

            if (isSignUpSuccesfull)
            {
                await NavigationService.PopAsync();
            }
        }

        public Command SignUpCommand { get; private set; }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
    }
}
