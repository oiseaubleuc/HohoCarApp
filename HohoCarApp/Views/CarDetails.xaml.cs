using HohoCarApp.Models;
using HohoCarApp.ViewModel;
using HohoCarApp.Services;

namespace HohoCarApp.Views
{
    public partial class CarDetails : ContentPage
    {
        public CarDetails(CarDetailsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);


        }
    }
}



