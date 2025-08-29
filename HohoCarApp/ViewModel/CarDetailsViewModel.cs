using HohoCarApp.Models;
using HohoCarApp.Services;

namespace HohoCarApp.ViewModel
{
    public class CarDetailsViewModel : BaseViewModel
    {

        private readonly ICarService _carService;

        public Car SelectedCar { get; private set; }


        public CarDetailsViewModel (ICarService carService)
        {
         _carService = carService;
        }



        public async Task LoadCarById(int id)
        {
            IsBusy = true;

            try
            {
                System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Loading car with ID {id}");
                SelectedCar = await _carService.GetCarByIdAsync(id);
                
                if (SelectedCar != null)
                {
                    SelectedCar.Brand ??= "Unknown Brand";
                    SelectedCar.Model ??= "Unknown Model";
                    SelectedCar.Description ??= "No description available";
                    SelectedCar.ImageUrl ??= string.Empty;
                    SelectedCar.Category ??= "SUV";
                    SelectedCar.FuelType ??= "Diesel";
                    SelectedCar.Location ??= "Unknown Location";
                    if (SelectedCar.Year <= 0) SelectedCar.Year = DateTime.Now.Year;
                    if (SelectedCar.Mileage < 0) SelectedCar.Mileage = 0;
                    if (SelectedCar.Views < 0) SelectedCar.Views = 0;
                    
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Loaded car {SelectedCar.Brand} {SelectedCar.Model}");
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Image URL: {SelectedCar.ImageUrl}");
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Price: {SelectedCar.Price}");
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Year: {SelectedCar.Year}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: No car found with ID {id}");
                }
                
                OnPropertyChanged(nameof(SelectedCar));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Error loading car {id}: {ex.Message}");
            }
            finally 
            {
                IsBusy = false;
            }
        }
       
    }
}
