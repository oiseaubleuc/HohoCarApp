using HohoCarApp.ViewModel;
using HohoCarApp.Models;
using HohoCarApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace HohoCarApp.Views
{
    public partial class CarList : ContentPage
    {
        private readonly CarListViewModel _viewModel;
        public CarList()
        {
            InitializeComponent();
            var carService = App.ServiceProvider.GetRequiredService<ICarService>();
            _viewModel = new CarListViewModel(carService);
            BindingContext = _viewModel;
        }
        public CarList(ICarService carService)
        {
            InitializeComponent();
            _viewModel = new CarListViewModel(carService);
            BindingContext = _viewModel;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Home");
        }

        private async void OnCarsClicked(object sender, EventArgs e)
        {
        }

        private async void OnNewsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///News");
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Login");
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchText = e.NewTextValue;
            _viewModel.ApplyFilters();
        }

        private void OnCategoryFilterClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                AllCategoryButton.BackgroundColor = Color.FromHex("#666");
                AllCategoryButton.TextColor = Colors.White;
                SuvCategoryButton.BackgroundColor = Colors.White;
                SuvCategoryButton.TextColor = Color.FromHex("#666");
                SedanCategoryButton.BackgroundColor = Colors.White;
                SedanCategoryButton.TextColor = Color.FromHex("#666");
                HatchbackCategoryButton.BackgroundColor = Colors.White;
                HatchbackCategoryButton.TextColor = Color.FromHex("#666");
                LuxuryCategoryButton.BackgroundColor = Colors.White;
                LuxuryCategoryButton.TextColor = Color.FromHex("#666");

                button.BackgroundColor = Color.FromHex("#007bff");
                button.TextColor = Colors.White;

                _viewModel.SelectedCategory = button.Text;
                _viewModel.ApplyFilters();
            }
        }

        private void OnFuelFilterClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                AllFuelButton.BackgroundColor = Color.FromHex("#666");
                AllFuelButton.TextColor = Colors.White;
                PetrolFuelButton.BackgroundColor = Colors.White;
                PetrolFuelButton.TextColor = Color.FromHex("#666");
                DieselFuelButton.BackgroundColor = Colors.White;
                DieselFuelButton.TextColor = Color.FromHex("#666");
                ElectricFuelButton.BackgroundColor = Colors.White;
                ElectricFuelButton.TextColor = Color.FromHex("#666");
                HybridFuelButton.BackgroundColor = Colors.White;
                HybridFuelButton.TextColor = Color.FromHex("#666");

                button.BackgroundColor = Color.FromHex("#007bff");
                button.TextColor = Colors.White;

                _viewModel.SelectedFuelType = button.Text;
                _viewModel.ApplyFilters();
            }
        }
    }
}
