using HohoCarApp.ViewModel;
using HohoCarApp.Services;

namespace HohoCarApp.Views
{
    public partial class Profile : ContentPage
    {
        private readonly ProfileViewModel _viewModel;

        public Profile()
        {
            InitializeComponent();
            
            var authService = App.ServiceProvider.GetRequiredService<IAuthService>();
            var favoriteService = App.ServiceProvider.GetRequiredService<IFavoriteService>();
            
            _viewModel = new ProfileViewModel(authService, favoriteService);
            BindingContext = _viewModel;
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Home");
        }

        private async void OnCarsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CarList");
        }

        private async void OnNewsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("News");
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
        }
    }
}
