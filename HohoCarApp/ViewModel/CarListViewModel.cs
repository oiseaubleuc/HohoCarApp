using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HohoCarApp.Models;
using HohoCarApp.ViewModel;

public class CarListViewModel : BaseViewModel
{
    private readonly ApiService _apiService;
    private ObservableCollection<Car> _cars;

    public ObservableCollection<Car> Cars
    {
        get { return _cars; }
        set
        {
            _cars = value;
            OnPropertyChanged();
        }
    }

    public CarListViewModel(ApiService apiService)
    {
        _apiService = apiService;
        Cars = new ObservableCollection<Car>();
        LoadCars();
    }

    public async Task LoadCars()
    {
        var cars = await _apiService.GetCarsAsync();
        foreach (var car in cars)
        {
            Cars.Add(car);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
