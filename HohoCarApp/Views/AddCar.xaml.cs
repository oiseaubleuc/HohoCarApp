using HohoCarApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace HohoCarApp.Views
{
    public partial class AddCar : ContentPage
    {
        private readonly AddCarViewModel _viewModel;

        public AddCar()
        {
            InitializeComponent();
            _viewModel = App.ServiceProvider.GetRequiredService<AddCarViewModel>();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
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
            await Shell.Current.GoToAsync("///Login");
        }
    }
}
