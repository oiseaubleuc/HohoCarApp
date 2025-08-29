using System.Net.Http;
using System.Net.Http.Json;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public ApiCategoryService(HttpClient httpClient)
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

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
                return result ?? new List<Category>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching categories from API: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _httpClient.GetFromJsonAsync<Category>($"api/categories/{id}");
                return category;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching category {id} from API: {ex.Message}");
                return null;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/categories", category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding category to API: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"api/categories/{category.Id}", category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating category in API: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/categories/{id}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting category from API: {ex.Message}");
                throw;
            }
        }
    }
}

