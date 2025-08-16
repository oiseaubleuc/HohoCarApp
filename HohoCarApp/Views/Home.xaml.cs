using HohoCarApp.ViewModel;
using HohoCarApp.Services;


namespace HohoCarApp.Views;


public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();

        //BindingContext = new HomeViewModel();

    }
    private async void OnBekijkAutosClicked(object sender, EventArgs e)
    {
        var vm = App.ServiceProvider.GetRequiredService<CarListViewModel>();
        await Navigation.PushAsync(new CarList(vm));
    }
}

