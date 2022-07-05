using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Views;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class UserProfileViewModel: BaseViewModel
    {
        private long id;
        private string userName;
        private string password;
        private string email;
        private string surname;
        private string lastname;
        private string address;
        private string phone;

        public UserProfileViewModel(INavigation navigation, IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            SignOutCommand = new Command(async () => await SignOut());

            Username = "Test";
            Surname = "Test";
            Lastname = "Test";
            Phone = "+1 444 4444444";
            Email = "test@gmail.com";
            Password = "1234";
        }

        public ICommand SignOutCommand { get; private set; }

        public async Task SignOut()
        {
            Console.WriteLine("Sign out.....");

            App.Current.MainPage = new NavigationPage(new SignInView());
        }

        public string Username
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        public string Lastname
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
    }
}
