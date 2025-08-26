using HohoCarApp.Views;

namespace HohoCarApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CarList), typeof(CarList));
            Routing.RegisterRoute(nameof(CarDetails), typeof(CarDetails));
            Routing.RegisterRoute(nameof(About), typeof(About));
            Routing.RegisterRoute(nameof(News), typeof(News));
        }
    }
}
