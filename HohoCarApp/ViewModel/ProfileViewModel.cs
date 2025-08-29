using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HohoCarApp.Models;
using HohoCarApp.Services;

namespace HohoCarApp.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        private readonly IFavoriteService _favoriteService;
        private User _userInfo;
        private ObservableCollection<Car> _favoriteCars;
        private bool _isLoading;
        private bool _isAdmin;

        public ProfileViewModel(IAuthService authService, IFavoriteService favoriteService)
        {
            _authService = authService;
            _favoriteService = favoriteService;
            _favoriteCars = new ObservableCollection<Car>();

            LogoutCommand = new Command(async () => await LogoutAsync());
            RemoveFavoriteCommand = new Command<Car>(async (car) => await RemoveFavoriteAsync(car));
            BrowseCarsCommand = new Command(async () => await BrowseCarsAsync());
            AddNewCarCommand = new Command(async () => await AddNewCarAsync());
            ManageUsersCommand = new Command(async () => await ManageUsersAsync());

            LoadProfileData();
        }

        public User UserInfo
        {
            get => _userInfo;
            set
            {
                _userInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Car> FavoriteCars
        {
            get => _favoriteCars;
            set
            {
                _favoriteCars = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FavoriteCarsCount));
                OnPropertyChanged(nameof(HasFavoriteCars));
                OnPropertyChanged(nameof(HasNoFavoriteCars));
            }
        }

        public int FavoriteCarsCount => FavoriteCars?.Count ?? 0;

        public bool HasFavoriteCars => FavoriteCarsCount > 0;

        public bool HasNoFavoriteCars => FavoriteCarsCount == 0;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogoutCommand { get; }
        public ICommand RemoveFavoriteCommand { get; }
        public ICommand BrowseCarsCommand { get; }
        public ICommand AddNewCarCommand { get; }
        public ICommand ManageUsersCommand { get; }

        private async void LoadProfileData()
        {
            try
            {
                IsLoading = true;

                var user = await _authService.GetCurrentUserAsync();
                if (user != null)
                {
                    UserInfo = user;
                    
                    IsAdmin = user.Id == 1 || user.Id == 2 || 
                              user.Email?.ToLower().Contains("admin") == true || 
                              user.Username?.ToLower().Contains("admin") == true;
                }

                await LoadFavoriteCars();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadFavoriteCars()
        {
            try
            {
                var favorites = await _favoriteService.GetUserFavoritesAsync();
                FavoriteCars.Clear();
                
                foreach (var favorite in favorites)
                {
                    FavoriteCars.Add(favorite.Car);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LogoutAsync()
        {
            try
            {
                var result = await Application.Current.MainPage.DisplayAlert(
                    "Logout", 
                    "Are you sure you want to logout?", 
                    "Yes", "No");

                if (result)
                {
                    await _authService.LogoutAsync();
                    await Shell.Current.GoToAsync("///Login");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "An error occurred during logout. Please try again.", 
                    "OK");
            }
        }

        private async Task RemoveFavoriteAsync(Car car)
        {
            try
            {
                var result = await Application.Current.MainPage.DisplayAlert(
                    "Remove Favorite", 
                    $"Are you sure you want to remove {car.Brand} {car.Model} from your favorites?", 
                    "Yes", "No");

                if (result)
                {
                    await _favoriteService.RemoveFavoriteAsync(car.Id);
                    FavoriteCars.Remove(car);
                    
                    await Application.Current.MainPage.DisplayAlert(
                        "Success", 
                        "Car removed from favorites", 
                        "OK");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "An error occurred while removing the favorite. Please try again.", 
                    "OK");
            }
        }

        private async Task BrowseCarsAsync()
        {
            await Shell.Current.GoToAsync("CarList");
        }

        private async Task AddNewCarAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("AddCar");
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "An error occurred while navigating to the add car page. Please try again.", 
                    "OK");
            }
        }

        private async Task ManageUsersAsync()
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Manage Users", 
                    "This feature will be implemented soon. You can manage users through the web admin panel.", 
                    "OK");
            }
            catch (Exception ex)
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
