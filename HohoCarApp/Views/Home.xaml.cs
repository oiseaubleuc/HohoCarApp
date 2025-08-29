using HohoCarApp.ViewModel;
using HohoCarApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace HohoCarApp.Views;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
        try
        {
            var carService = App.ServiceProvider?.GetRequiredService<ICarService>();
            if (carService != null)
            {
                BindingContext = new HomeViewModel(carService);
            }
            else
            {
                DisplayAlert("Error", "Car service is not available", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Failed to initialize app: " + ex.Message, "OK");
        }
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
    }

    private async void OnCarsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CarList");
    }

    private async void OnNewsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///News");
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Login");
    }
}

