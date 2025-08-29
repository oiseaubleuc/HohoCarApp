using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HohoCarApp.Services;
using HohoCarApp.Models;

namespace HohoCarApp.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private string _email;
        private string _password;
        private bool _rememberMe;
        private bool _isLoading;
        private string _errorMessage;
        private bool _hasError;

        public LoginViewModel()
        {
            _authService = App.ServiceProvider.GetRequiredService<IAuthService>();
            
            LoginCommand = new Command(async () => await LoginAsync());
            ForgotPasswordCommand = new Command(async () => await ForgotPasswordAsync());
            NavigateToRegisterCommand = new Command(async () => await NavigateToRegisterAsync());
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

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
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

        public ICommand LoginCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public void OnAppearing()
        {
            ErrorMessage = string.Empty;
            
            CheckExistingLogin();
        }

        private async Task LoginAsync()
        {
            System.Diagnostics.Debug.WriteLine("=== LOGIN ATTEMPT STARTED ===");
            System.Diagnostics.Debug.WriteLine($"Email: {Email}");
            System.Diagnostics.Debug.WriteLine($"Password: {Password}");
            
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both email and password.";
                System.Diagnostics.Debug.WriteLine("Validation failed: Empty email or password");
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var loginResult = await _authService.LoginAsync(Email, Password, RememberMe);
                
                if (loginResult.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Login Successful", 
                        "Welcome back! You have been successfully logged in.", 
                        "OK");
                    
                    await Shell.Current.GoToAsync("///Profile");
                }
                else
                {
                    ErrorMessage = loginResult.ErrorMessage ?? "Login failed. Please check your credentials.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during login. Please try again.";
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }



        private async Task ForgotPasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Please enter your email address first.";
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var result = await _authService.ForgotPasswordAsync(Email);
                
                if (result.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Password Reset", 
                        "If an account with this email exists, you will receive a password reset link.", 
                        "OK");
                }
                else
                {
                    ErrorMessage = result.ErrorMessage ?? "Failed to send password reset email.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred. Please try again.";
                System.Diagnostics.Debug.WriteLine($"Forgot password error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("Register");
        }

        private async void CheckExistingLogin()
        {
            try
            {
                var isLoggedIn = await _authService.IsLoggedInAsync();
                if (isLoggedIn)
                {
                    await Shell.Current.GoToAsync("///Profile");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Check existing login error: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
