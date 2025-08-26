using HohoCarApp.ViewModel;
using HohoCarApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HohoCarApp.Views;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
        var carService = App.ServiceProvider.GetRequiredService<ICarService>();
        BindingContext = new HomeViewModel(carService);
    }
}

