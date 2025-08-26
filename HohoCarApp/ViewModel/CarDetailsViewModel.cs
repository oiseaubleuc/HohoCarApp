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
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: Loaded car {SelectedCar.Brand} {SelectedCar.Model}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"CarDetailsViewModel: No car found with ID {id}");
                }
                
                OnPropertyChanged(nameof(SelectedCar));
            }
            finally 
            {
                IsBusy = false;
            }

        }
       
    }
}
