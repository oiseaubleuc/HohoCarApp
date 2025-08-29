using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HohoCarApp.Services;
using HohoCarApp.Models;

namespace HohoCarApp.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private bool _acceptTerms;
        private bool _isLoading;
        private string _errorMessage;
        private bool _hasError;

        public RegisterViewModel()
        {
            _authService = App.ServiceProvider.GetRequiredService<IAuthService>();
            
            RegisterCommand = new Command(async () => await RegisterAsync());
            ViewTermsCommand = new Command(async () => await ViewTermsAsync());
            NavigateToLoginCommand = new Command(async () => await NavigateToLoginAsync());
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public bool AcceptTerms
        {
            get => _acceptTerms;
            set
            {
                _acceptTerms = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                HasError = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewTermsCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public void OnAppearing()
        {
            ErrorMessage = string.Empty;
        }

        private async Task RegisterAsync()
        {
            if (!ValidateInputs())
                return;

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var registerRequest = new Models.RegisterRequest
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Username = Username,
                    Password = Password
                };

                var registerResult = await _authService.RegisterAsync(registerRequest);
                
                if (registerResult.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Registration Successful", 
                        "Your account has been created successfully. Welcome to HohoCarApp!", 
                        "OK");
                    
                    await Shell.Current.GoToAsync("///Profile");
                }
                else
                {
                    ErrorMessage = registerResult.ErrorMessage ?? "Registration failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during registration. Please try again.";
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                ErrorMessage = "Please enter your first name.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                ErrorMessage = "Please enter your last name.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Please enter your email address.";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Please enter a valid email address.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                ErrorMessage = "Please enter your phone number.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Please choose a username.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter a password.";
                return false;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters long.";
                return false;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return false;
            }

            if (!AcceptTerms)
            {
                ErrorMessage = "Please accept the Terms and Conditions.";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task ViewTermsAsync()
        {
            await Application.Current.MainPage.DisplayAlert(
                "Terms and Conditions",
                "By using this app, you agree to our terms of service and privacy policy. " +
                "We respect your privacy and will protect your personal information according to our privacy policy.",
                "OK");
        }

        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("Login");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
