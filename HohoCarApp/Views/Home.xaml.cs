using HohoCarApp.ViewModel;
using HohoCarApp.Services;


namespace HohoCarApp.Views;


public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();

        BindingContext = new HomeViewModel();

    }
}