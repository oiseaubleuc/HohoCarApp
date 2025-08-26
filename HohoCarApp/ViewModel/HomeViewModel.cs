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
        public ICommand NavigateToAboutCommand { get; }
        public ICommand NavigateToNewsCommand { get; }

        public HomeViewModel(ICarService carService)
        {
            this.carService = carService;
            NavigateToCarListCommand = new Command(async () => await GoToCarList());
            NavigateToAboutCommand = new Command(async () => await GoToAbout());
            NavigateToNewsCommand = new Command(async () => await GoToNews());
        }

        private async Task GoToCarList()
        {
            await Shell.Current.GoToAsync(nameof(CarList));
        }

        private async Task GoToAbout()
        {
            await Shell.Current.GoToAsync(nameof(About));
        }

        private async Task GoToNews()
        {
            await Shell.Current.GoToAsync(nameof(News));
        }
    }
}

