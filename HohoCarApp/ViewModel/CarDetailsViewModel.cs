using HohoCarApp.Models;

namespace HohoCarApp.ViewModel
{
    public class CarDetailsViewModel : BaseViewModel
    {
        private Car selectedCar;
        public Car SelectedCar
        {
            get => selectedCar;
            set => SetProperty(ref selectedCar, value);
        }

        public CarDetailsViewModel(Car car)
        {
            SelectedCar = car;
        }
    }
}
