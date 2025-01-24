using HohoCarApp.ViewModel;
using HohoCarApp.Models;

namespace HohoCarApp.Views
{
  

    public partial class CarList : ContentPage
    {
        private readonly CarListViewModel _viewModel;

        public CarList(CarListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Car selectedCar)
            {
                await Shell.Current.GoToAsync($"{nameof(CarDetails)}?CarId={selectedCar.Id}");
            }
        }
    }

}
