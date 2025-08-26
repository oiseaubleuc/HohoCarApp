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
			var result = await _httpClient.GetFromJsonAsync<List<Car>>("api/cars");
			var cars = result ?? new List<Car>();
			NormalizeImageUrls(cars);
			return cars;
		}

		public async Task<Car> GetCarByIdAsync(int carId)
		{
			var car = await _httpClient.GetFromJsonAsync<Car>($"api/cars/{carId}");
			if (car != null) NormalizeImageUrl(car);
			return car;
		}

		public async Task AddCarAsync(Car car)
		{
			await _httpClient.PostAsJsonAsync("api/cars", car);
		}

		public async Task UpdateCarAsync(Car car)
		{
			await _httpClient.PutAsJsonAsync($"api/cars/{car.Id}", car);
		}

		public async Task DeleteCarAsync(int id)
		{
			await _httpClient.DeleteAsync($"api/cars/{id}");
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
			// make relative URLs absolute using BaseAddress
			var baseUri = _httpClient.BaseAddress ?? new Uri("https://localhost:7231/");
			car.ImageUrl = new Uri(baseUri, car.ImageUrl).ToString();
		}
	}
}


