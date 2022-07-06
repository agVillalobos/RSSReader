using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using RSSFeed.Interfaces;
using RSSFeed.Rules;
using RSSFeed.Validations;
using Xamarin.Forms;

namespace RSSFeed.ViewModels
{
    public class SignUpViewModel: BaseViewModel
    {
        private readonly IUserService userService;
        private ValidatableObject<string> username;
        private ValidatableObject<string> surname;
        private ValidatableObject<string> phone;
        private ValidatableObject<string> password;
        private ValidatableObject<string> email;
        private ValidatableObject<string> lastname;

        public SignUpViewModel(INavigation navigation, IUserService userService, IUserDialogs userDialogs) : base(navigation, userDialogs)
        {
            Username = new ValidatableObject<string>();
            Surname = new ValidatableObject<string>();
            Phone = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();
            Lastname = new ValidatableObject<string>();

            this.userService = userService;

            SignUpCommand = new Command(async () => await SingUp());

            AddValidations();
        }

        private async Task SingUp()
        {
            if (!AreFieldsValid()) return;

            var isSignUpSuccesfull = await userService.SignUp(new Models.User(Username.Value, Password.Value, Email.Value, Surname.Value, Lastname.Value, "", Phone.Value));


            if (isSignUpSuccesfull)
            {
                SignUpCommand.CanExecute(false);
                UserDialogs.Toast("User added successfully", TimeSpan.FromSeconds(2));
                await Task.Delay(2000);

                await NavigationService.PopAsync();
            }
            else
            {
                UserDialogs.Toast("Error while trying to add User. Please try again.", TimeSpan.FromSeconds(2));
            }

        }

        private bool AreFieldsValid()
        {
            var isUserNameValid = Username.Validate();
            var isSurnameValid = Surname.Validate();
            var isPhoneValid = Phone.Validate();
            var isPasswordValid = Password.Validate();
            var isEmailValid = Email.Validate();
            var isLastNameValid = Lastname.Validate();

            return isUserNameValid && isSurnameValid && isPhoneValid && isPasswordValid && isEmailValid && isLastNameValid;
        }

        public void AddValidations()
        {
            Username.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Username is required"});
            Surname.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Surname is required" });
            Phone.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Phone is required" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Password is required" });
            Email.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Email is required" });
            Lastname.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Lastname is required" });
        }

        public ICommand SignUpCommand { get; private set; }

        public ValidatableObject<string> Username
        {
            get => username;
            set => SetProperty(ref username, value);
        } 

        public ValidatableObject<string> Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        public ValidatableObject<string> Lastname
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }

        public ValidatableObject<string> Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public ValidatableObject<string> Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ValidatableObject<string> Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
    }
}
