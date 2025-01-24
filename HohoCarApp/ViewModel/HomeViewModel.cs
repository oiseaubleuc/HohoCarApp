using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using HohoCarApp.Views;
using HohoCarApp.Services;

namespace HohoCarApp.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICarService carService;

        public ICommand NavigateToCarListCommand { get; }

        public HomeViewModel(ICarService carService)
        {
            this.carService = carService;

            NavigateToCarListCommand = new Command(async () => await GoToCarList());
        }

        private async Task GoToCarList()
        {
            await Shell.Current.GoToAsync(nameof(CarList));
        }
    }

}

