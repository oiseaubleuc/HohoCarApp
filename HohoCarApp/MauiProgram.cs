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

            // HTTP client and services
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ICarService, ApiCarService>();
            builder.Services.AddSingleton<ICategoryService, ApiCategoryService>();
            builder.Services.AddSingleton<IFuelTypeService, ApiFuelTypeService>();
            builder.Services.AddSingleton<IAuthService, ApiAuthService>();
            builder.Services.AddSingleton<IFavoriteService, ApiFavoriteService>();
            builder.Services.AddSingleton<ISecureStorage>(SecureStorage.Default);

            builder.Services.AddTransient<CarListViewModel>();
            builder.Services.AddTransient<CarDetailsViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<AddCarViewModel>();
            builder.Services.AddTransient<CarList>();
            builder.Services.AddTransient<CarDetails>();
            builder.Services.AddTransient<Home>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<Register>();
            builder.Services.AddTransient<Profile>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            App.ServiceProvider = app.Services;

            return app;
        }
    }
}
    

