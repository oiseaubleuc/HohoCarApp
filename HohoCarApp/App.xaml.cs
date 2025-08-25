using HohoCarApp.Views;

namespace HohoCarApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;

            MainPage = new NavigationPage(new Home()); 
        }

    }
}


