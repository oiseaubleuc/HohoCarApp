using HohoCarApp.ViewModel;
using HohoCarApp.Models;
using HohoCarApp.Services;

namespace HohoCarApp.Views
{
    public partial class CarList : ContentPage
    {
        private readonly CarListViewModel _viewModel;
        private readonly CarListViewModel _vm;


        public CarList(ApiService apiService) // Verander de constructor om ApiService door te geven
        {
            InitializeComponent();
            _viewModel = new CarListViewModel(apiService); // Maak een nieuwe instantie van CarListViewModel
            BindingContext = _viewModel; // Zet de BindingContext naar de ViewModel
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
            {
                await Shell.Current.GoToAsync($"{nameof(CarDetails)}?CarId={selectedCar.Id}");
            }
        }
        public CarList(CarListViewModel vm)
        {
            InitializeComponent();
            BindingContext = _vm = vm;
        }
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
