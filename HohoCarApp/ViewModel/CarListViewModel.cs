using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HohoCarApp.Models;

namespace HohoCarApp.ViewModel
{
    public class CarListViewModel : BaseViewModel
    {
        private ObservableCollection<Car> cars;
        public ObservableCollection<Car> Cars
        {
            get => cars;
            set => SetProperty(ref cars, value);
        }

        public CarListViewModel()
        {
            Cars = new ObservableCollection<Car>();
            LoadCars();
        }

        private async Task LoadCars()
        {
            // Simuleer ophalen van data. Later vervangen door een echte database-aanroep.
            await Task.Delay(1000);
            Cars.Add(new Car { Brand = "Toyota", Model = "Corolla", Price = 20000, Year = 2022, Mileage = 5000 });
            Cars.Add(new Car { Brand = "Tesla", Model = "Model 3", Price = 35000, Year = 2021, Mileage = 2000 });
            Cars.Add(new Car { Brand = "Ford", Model = "Focus", Price = 15000, Year = 2019, Mileage = 30000 });
        }
    }
}
