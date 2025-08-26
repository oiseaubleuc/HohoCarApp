using HohoCarApp.Models;
using HohoCarApp.ViewModel;
using HohoCarApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HohoCarApp.Views
{
	[QueryProperty(nameof(CarId), "CarId")]
	public partial class CarDetails : ContentPage
	{
		private readonly CarDetailsViewModel _viewModel;

		public CarDetails()
		{
			InitializeComponent();
			var carService = App.ServiceProvider.GetRequiredService<ICarService>();
			_viewModel = new CarDetailsViewModel(carService);
			BindingContext = _viewModel;
		}

		public CarDetails(CarDetailsViewModel vm)
		{
			InitializeComponent();
			BindingContext = _viewModel = vm;
		}

		public string CarId
		{
			get => string.Empty;
			set
			{
				if (int.TryParse(value, out var id))
				{
					_ = _viewModel.LoadCarById(id);
				}
			}
		}

		private async void OnBackToListClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("..");
		}
	}
}



