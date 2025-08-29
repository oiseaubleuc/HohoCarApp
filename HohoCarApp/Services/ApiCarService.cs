using System.Net.Http;
using System.Net.Http.Json;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
	public class ApiCarService : ICarService
	{
		private readonly HttpClient _httpClient;

		public ApiCarService(HttpClient httpClient)
		{
			_httpClient = httpClient;

			if (_httpClient.BaseAddress == null)
			{
				var baseUrl = "https://localhost:7231/";

				#if ANDROID
				baseUrl = "https://10.0.2.2:7231/";
				#endif

				_httpClient.BaseAddress = new Uri(baseUrl);
			}
		}

		public async Task<List<Car>> GetCarsAsync()
		{
			try
			{
				var result = await _httpClient.GetFromJsonAsync<List<Car>>("api/cars");
				var cars = result ?? new List<Car>();
				
				// Validate and normalize car data
				foreach (var car in cars)
				{
					ValidateAndNormalizeCar(car);
				}
				
				NormalizeImageUrls(cars);
				return cars;
			}
			catch (Exception ex)
			{
				// Log the error but return empty list instead of dummy data
				System.Diagnostics.Debug.WriteLine($"Error fetching cars from API: {ex.Message}");
				return new List<Car>();
			}
		}

		public async Task<Car> GetCarByIdAsync(int carId)
		{
			try
			{
				var car = await _httpClient.GetFromJsonAsync<Car>($"api/cars/{carId}");
				if (car != null)
				{
					ValidateAndNormalizeCar(car);
					NormalizeImageUrl(car);
				}
				return car;
			}
			catch (Exception ex)
			{

				
				return null;
			}
		}

		public async Task<Car> AddCarAsync(Car car)
		{
			try
			{
				                await Task.Delay(500);
				
				
				car.Id = new Random().Next(100, 999);
				car.CreatedAt = DateTime.Now;
				

				ValidateAndNormalizeCar(car);
				NormalizeImageUrl(car);
				
				
				
				return car;
			}
			catch (Exception ex)
			{
				
				return null;
			}
		}

		public async Task UpdateCarAsync(Car car)
		{
			try
			{
				await _httpClient.PutAsJsonAsync($"api/cars/{car.Id}", car);
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public async Task DeleteCarAsync(int id)
		{
			try
			{
				await _httpClient.DeleteAsync($"api/cars/{id}");
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		private void NormalizeImageUrls(IEnumerable<Car> cars)
		{
			foreach (var car in cars)
			{
				NormalizeImageUrl(car);
			}
		}

		private void NormalizeImageUrl(Car car)
		{
			if (string.IsNullOrWhiteSpace(car?.ImageUrl)) return;
			if (Uri.TryCreate(car.ImageUrl, UriKind.Absolute, out _)) return;
			
			var baseUri = _httpClient.BaseAddress ?? new Uri("https://localhost:7231/");
			car.ImageUrl = new Uri(baseUri, car.ImageUrl).ToString();
		}

		private void ValidateAndNormalizeCar(Car car)
		{
			if (car == null) return;


			car.Brand ??= string.Empty;
			car.Model ??= string.Empty;
			car.Description ??= string.Empty;
			car.ImageUrl ??= string.Empty;
			car.Category ??= "SUV";
			car.FuelType ??= "Diesel";
			car.Location ??= "Luik";

			if (car.Year <= 0) car.Year = DateTime.Now.Year;
			if (car.Mileage < 0) car.Mileage = 0;
			if (car.Views < 0) car.Views = 0;
		}
	}
}


