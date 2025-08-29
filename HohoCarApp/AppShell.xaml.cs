using HohoCarApp.Views;
using Microsoft.Maui.Controls;

namespace HohoCarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Register all routes for navigation
            Routing.RegisterRoute(nameof(CarList), typeof(CarList));
            Routing.RegisterRoute(nameof(CarDetails), typeof(CarDetails));
            Routing.RegisterRoute(nameof(News), typeof(News));

            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Register), typeof(Register));
            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
            Routing.RegisterRoute(nameof(AddCar), typeof(AddCar));
        }
    }
}
