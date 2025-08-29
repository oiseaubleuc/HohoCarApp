using Microsoft.Maui.Controls;
using HohoCarApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using HohoCarApp.Views;
using HohoCarApp.ViewModel;
using System.Diagnostics;
using System.Collections.ObjectModel;
using HohoCarApp.Models;

namespace HohoCarApp.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICarService carService;
        private bool _isLoading;

        public ObservableCollection<Car> Cars { get; } = new();
        public ICommand NavigateToCarListCommand { get; }
        public ICommand NavigateToNewsCommand { get; }
        public ICommand BrowseCarsCommand { get; }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public HomeViewModel(ICarService carService)
        {
            this.carService = carService;
            NavigateToCarListCommand = new Command(async () => await GoToCarList());
            NavigateToNewsCommand = new Command(async () => await GoToNews());
            BrowseCarsCommand = new Command(async () => await GoToCarList());
            _ = LoadCars();
        }

        private async Task GoToCarList()
        {
            await Shell.Current.GoToAsync(nameof(CarList));
        }

      
        private async Task GoToNews()
        {
            await Shell.Current.GoToAsync(nameof(News));
        }

        private async Task LoadCars()
        {
            try
            {
                IsLoading = true;
                var cars = await carService.GetCarsAsync();
                Cars.Clear();
                foreach (var car in cars)
                    Cars.Add(car);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading cars in HomeViewModel: {ex.Message}");
                Cars.Clear();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

