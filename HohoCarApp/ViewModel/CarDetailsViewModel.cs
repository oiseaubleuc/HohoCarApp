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
                SelectedCar = await _carService.GetCarByIdAsync(id); 
                OnPropertyChanged(nameof(SelectedCar));
            }
            finally 
            {
                IsBusy = false;
            }

        }
       
    }
}
