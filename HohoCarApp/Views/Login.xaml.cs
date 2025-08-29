using HohoCarApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace HohoCarApp.Views
{
    public partial class Login : ContentPage
    {
        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = App.ServiceProvider.GetRequiredService<LoginViewModel>();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== LOGIN BUTTON CLICKED ===");
            System.Diagnostics.Debug.WriteLine($"Button clicked at: {DateTime.Now}");
            System.Diagnostics.Debug.WriteLine($"ViewModel is null: {_viewModel == null}");
            System.Diagnostics.Debug.WriteLine($"LoginCommand is null: {_viewModel?.LoginCommand == null}");
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Home");
        }

        private async void OnCarsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CarList");
        }

        private async void OnNewsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///News");
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
        }

        private void OnEmailCompleted(object sender, EventArgs e)
        {
            if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
            {
                entry.Text = entry.Text.Trim();
            }
        }
    }
}
