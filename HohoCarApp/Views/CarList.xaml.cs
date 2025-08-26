using HohoCarApp.ViewModel;
using HohoCarApp.Models;
using HohoCarApp.Services;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
