using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HohoCarApp.Models;
using HohoCarApp.ViewModel;
using System.Threading.Tasks;
using HohoCarApp.Services;


using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HohoCarApp.Models;
using HohoCarApp.Services;

public class CarListViewModel : BaseViewModel
{
    private readonly ApiService _api;

   
    public ObservableCollection<Car> Cars { get; } = new();

 
    public CarListViewModel(ApiService api)
    {
        _api = api;          
        _ = LoadCars();    
    }

    public async Task LoadCars()
    {
        var cars = await _api.GetCarsAsync();   
        Cars.Clear();
        foreach (var car in cars)
            Cars.Add(car);
    }
}
