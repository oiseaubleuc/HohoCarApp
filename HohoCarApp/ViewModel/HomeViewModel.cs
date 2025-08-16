using Microsoft.Maui.Controls;
using HohoCarApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using HohoCarApp.Views;
using HohoCarApp.ViewModel;
using System.Diagnostics;

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

