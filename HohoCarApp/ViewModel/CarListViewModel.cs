using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HohoCarApp.Models;
using HohoCarApp.ViewModel;
using System.Threading.Tasks;
using HohoCarApp.Services;
using System.Windows.Input;

namespace HohoCarApp.ViewModel
{
    public class CarListViewModel : BaseViewModel
    {
        private readonly ICarService _carService;
        private bool _isLoading;
        private string _searchText = string.Empty;
        private string _selectedCategory = "All";
        private string _selectedFuelType = "All";
        private List<Car> _allCars = new();

        public ObservableCollection<Car> Cars { get; } = new();
        public ObservableCollection<Car> FilteredCars { get; } = new();
        public ICommand SeeDetailsCommand { get; }
        public ICommand RefreshCommand { get; }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public string SelectedFuelType
        {
            get => _selectedFuelType;
            set => SetProperty(ref _selectedFuelType, value);
        }

        public bool HasCars => FilteredCars.Count > 0;
        public bool HasNoCars => FilteredCars.Count == 0 && !IsLoading;

        public CarListViewModel(ICarService carService)
        {
            _carService = carService;
            SeeDetailsCommand = new Command<Car>(async (car) => await SeeDetails(car));
            RefreshCommand = new Command(async () => await LoadCars());
            _ = LoadCars();
        }

        private async Task SeeDetails(Car car)
        {
            if (car != null)
            {
                await Shell.Current.GoToAsync($"CarDetails?CarId={car.Id}");
            }
        }

        public async Task LoadCars()
        {
            try
            {
                IsLoading = true;
                OnPropertyChanged(nameof(HasCars));
                OnPropertyChanged(nameof(HasNoCars));

                var cars = await _carService.GetCarsAsync();
                _allCars = cars ?? new List<Car>();
                Cars.Clear();
                foreach (var car in _allCars)
                    Cars.Add(car);
                
                ApplyFilters();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading cars: {ex.Message}");
                _allCars.Clear();
                Cars.Clear();
                FilteredCars.Clear();
            }
            finally
            {
                IsLoading = false;
                OnPropertyChanged(nameof(HasCars));
                OnPropertyChanged(nameof(HasNoCars));
            }
        }

        public void ApplyFilters()
        {
            var filtered = _allCars.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(car =>
                    (car.Brand?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (car.Model?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (car.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            if (SelectedCategory != "All")
            {
                filtered = filtered.Where(car => 
                    !string.IsNullOrEmpty(car.Category) && 
                    car.Category.Equals(SelectedCategory, StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedFuelType != "All")
            {
                filtered = filtered.Where(car => 
                    !string.IsNullOrEmpty(car.FuelType) && 
                    car.FuelType.Equals(SelectedFuelType, StringComparison.OrdinalIgnoreCase));
            }

            FilteredCars.Clear();
            foreach (var car in filtered)
                FilteredCars.Add(car);

            OnPropertyChanged(nameof(HasCars));
            OnPropertyChanged(nameof(HasNoCars));
        }
    }
}
