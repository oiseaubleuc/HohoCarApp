using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HohoCarApp.Models;
using HohoCarApp.Services;
using Microsoft.Maui.Storage;

namespace HohoCarApp.ViewModel
{
    public class AddCarViewModel : INotifyPropertyChanged
    {
        private readonly ICarService _carService;
        private readonly IAuthService _authService;

        private string _brand = string.Empty;
        private string _model = string.Empty;
        private string _year = string.Empty;
        private string _price = string.Empty;
        private string _mileage = string.Empty;
        private string _selectedFuelType = string.Empty;
        private string _selectedCategory = string.Empty;
        private string _description = string.Empty;
        private string _imageUrl = string.Empty;
        private ImageSource _selectedImageSource;
        private string _selectedImagePath = string.Empty;
        private bool _isLoading;
        private string _errorMessage = string.Empty;
        private string _successMessage = string.Empty;

        public AddCarViewModel(ICarService carService, IAuthService authService)
        {
            _carService = carService;
            _authService = authService;
            AddCarCommand = new Command(async () => await AddCarAsync());
            SelectImageCommand = new Command(async () => await SelectImageAsync());
        }

        public string Brand
        {
            get => _brand;
            set => SetProperty(ref _brand, value);
        }

        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public string Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public string Mileage
        {
            get => _mileage;
            set => SetProperty(ref _mileage, value);
        }

        public string SelectedFuelType
        {
            get => _selectedFuelType;
            set => SetProperty(ref _selectedFuelType, value);
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public ImageSource SelectedImageSource
        {
            get => _selectedImageSource;
            set => SetProperty(ref _selectedImageSource, value);
        }

        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set => SetProperty(ref _selectedImagePath, value);
        }

        public bool HasSelectedImage => SelectedImageSource != null;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public bool HasSuccess => !string.IsNullOrEmpty(SuccessMessage);

        public ICommand AddCarCommand { get; }
        public ICommand SelectImageCommand { get; }

        public void OnAppearing()
        {
            ClearForm();
        }

        private async Task AddCarAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(Brand))
                {
                    ErrorMessage = "Brand is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Model))
                {
                    ErrorMessage = "Model is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Year))
                {
                    ErrorMessage = "Year is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Price))
                {
                    ErrorMessage = "Price is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(Mileage))
                {
                    ErrorMessage = "Mileage is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedFuelType))
                {
                    ErrorMessage = "Fuel type is required";
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedCategory))
                {
                    ErrorMessage = "Category is required";
                    return;
                }

                if (!int.TryParse(Year, out int yearValue))
                {
                    ErrorMessage = "Invalid year format";
                    return;
                }

                if (!decimal.TryParse(Price, out decimal priceValue))
                {
                    ErrorMessage = "Invalid price format";
                    return;
                }

                if (!int.TryParse(Mileage, out int mileageValue))
                {
                    ErrorMessage = "Invalid mileage format";
                    return;
                }

                string imageUrl = string.Empty;
                if (!string.IsNullOrEmpty(SelectedImagePath))
                {
                    imageUrl = SelectedImagePath;
                }
                else if (!string.IsNullOrEmpty(ImageUrl?.Trim()))
                {
                    imageUrl = ImageUrl.Trim(); 
                }

                var newCar = new Car
                {
                    Brand = Brand.Trim(),
                    Model = Model.Trim(),
                    Year = yearValue,
                    Price = priceValue,
                    Mileage = mileageValue,
                    FuelType = SelectedFuelType,
                    Category = SelectedCategory,
                    Description = Description?.Trim() ?? string.Empty,
                    ImageUrl = imageUrl,
                    CreatedAt = DateTime.Now
                };

                // Add car using service
                var addedCar = await _carService.AddCarAsync(newCar);

                if (addedCar != null)
                {
                    SuccessMessage = "Car added successfully!";
                    ClearForm();
                    
                    await Task.Delay(1500);
                    await Shell.Current.GoToAsync("CarList");
                }
                else
                {
                    ErrorMessage = "Failed to add car. Please try again.";
                }
            }
            catch (Exception ex)
            {

                ErrorMessage = "An error occurred while adding the car.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SelectImageAsync()
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Select Car Image",
                    FileTypes = FilePickerFileType.Images
                };

                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    SelectedImagePath = result.FullPath;
                    SelectedImageSource = ImageSource.FromFile(result.FullPath);
                    
                    ImageUrl = string.Empty;
                    
    
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "An error occurred while selecting the image. Please try again.", 
                    "OK");
            }
        }

        private void ClearForm()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Year = string.Empty;
            Price = string.Empty;
            Mileage = string.Empty;
            SelectedFuelType = string.Empty;
            SelectedCategory = string.Empty;
            Description = string.Empty;
            ImageUrl = string.Empty;
            SelectedImageSource = null;
            SelectedImagePath = string.Empty;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
