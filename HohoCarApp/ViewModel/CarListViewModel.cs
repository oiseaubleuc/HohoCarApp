using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HohoCarApp.Models;
using HohoCarApp.ViewModel;
using System.Threading.Tasks;
using HohoCarApp.Services;
using System.Windows.Input;

namespace HohoCarApp.ViewModel
{
    public class CarListViewModel : BaseViewModel
    {
        private readonly ICarService _carService;

     
        public ObservableCollection<Car> Cars { get; } = new();

        public ICommand SeeDetailsCommand { get; }

     
        public CarListViewModel(ICarService carService)
        {
            _carService = carService;
            SeeDetailsCommand = new Command<Car>(async (car) => await SeeDetails(car));
            _ = LoadCars();
        }

        private async Task SeeDetails(Car car)
        {
            if (car != null)
            {
                await Shell.Current.GoToAsync($"CarDetails?CarId={car.Id}");
            }
        }

        public async Task LoadCars()
        {
            var cars = await _carService.GetCarsAsync();
            Cars.Clear();
            foreach (var car in cars)
                Cars.Add(car);
        }
    }
}
