using HohoCarApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace HohoCarApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;

            MainPage = new AppShell(); 
        }

    }
}


