using HohoCarApp.Services;
using HohoCarApp.ViewModel;
using HohoCarApp.Views;
using Microsoft.Extensions.Logging;
using System.Net.Http;


namespace HohoCarApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ICarService, CarService>();
            builder.Services.AddTransient<CarListViewModel>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddTransient<CarList>();
            builder.Services.AddTransient<CarDetails>();

            builder.Services.AddHttpClient();                 
            builder.Services.AddSingleton<ApiService>();     
            builder.Services.AddTransient<CarListViewModel>(); 
            builder.Services.AddTransient<CarList>();        




#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();


            App.ServiceProvider = app.Services;

            return builder.Build();
        }
    }
}
    

