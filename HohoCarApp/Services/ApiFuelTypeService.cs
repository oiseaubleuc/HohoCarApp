using System.Net.Http;
using System.Net.Http.Json;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public class ApiFuelTypeService : IFuelTypeService
    {
        private readonly HttpClient _httpClient;

        public ApiFuelTypeService(HttpClient httpClient)
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

        public async Task<List<FuelType>> GetFuelTypesAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<FuelType>>("api/fueltypes");
                return result ?? new List<FuelType>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching fuel types from API: {ex.Message}");
                return new List<FuelType>();
            }
        }

        public async Task<FuelType> GetFuelTypeByIdAsync(int id)
        {
            try
            {
                var fuelType = await _httpClient.GetFromJsonAsync<FuelType>($"api/fueltypes/{id}");
                return fuelType;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching fuel type {id} from API: {ex.Message}");
                return null;
            }
        }

        public async Task AddFuelTypeAsync(FuelType fuelType)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/fueltypes", fuelType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding fuel type to API: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateFuelTypeAsync(FuelType fuelType)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"api/fueltypes/{fuelType.Id}", fuelType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating fuel type in API: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteFuelTypeAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/fueltypes/{id}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting fuel type from API: {ex.Message}");
                throw;
            }
        }
    }
}

