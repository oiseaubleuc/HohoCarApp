using System.Net.Http;
using System.Net.Http.Json;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public class ApiFavoriteService : IFavoriteService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecureStorage _secureStorage;

        public ApiFavoriteService(HttpClient httpClient, ISecureStorage secureStorage)
        {
            _httpClient = httpClient;
            _secureStorage = secureStorage;

            if (_httpClient.BaseAddress == null)
            {
                var baseUrl = "https://localhost:7231/";

                #if ANDROID
                baseUrl = "https://10.0.2.2:7231/";
                #endif

                _httpClient.BaseAddress = new Uri(baseUrl);
            }
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync()
        {
            try
            {
                await Task.Delay(100); 
                
                var userId = await _secureStorage.GetAsync("user_id");
                if (string.IsNullOrEmpty(userId) || userId == "")
                    return new List<Favorite>();

             
                return userId switch
                {
                    "1" => new List<Favorite>
                    {
                        new Favorite { Id = 1, UserId = 1, CarId = 1, CreatedAt = DateTime.Now.AddDays(-5) },
                        new Favorite { Id = 2, UserId = 1, CarId = 3, CreatedAt = DateTime.Now.AddDays(-2) }
                    },
                    "2" => new List<Favorite>
                    {
                        new Favorite { Id = 3, UserId = 2, CarId = 2, CreatedAt = DateTime.Now.AddDays(-10) },
                        new Favorite { Id = 4, UserId = 2, CarId = 4, CreatedAt = DateTime.Now.AddDays(-1) }
                    },
                    _ => new List<Favorite>()
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching user favorites: {ex.Message}");
                return new List<Favorite>();
            }
        }

        public async Task<Favorite> AddFavoriteAsync(int carId)
        {
            try
            {
                await Task.Delay(100); 
                
                var userId = await _secureStorage.GetAsync("user_id");
                if (string.IsNullOrEmpty(userId) || userId == "")
                    return null;

                var favorite = new Favorite
                {
                    Id = new Random().Next(100, 999),
                    UserId = int.Parse(userId),
                    CarId = carId,
                    CreatedAt = DateTime.Now
                };

                System.Diagnostics.Debug.WriteLine($"Mock favorite added: Car {carId} for User {userId}");
                return favorite;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding favorite: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveFavoriteAsync(int carId)
        {
            try
            {
                await Task.Delay(100);
                
                var userId = await _secureStorage.GetAsync("user_id");
                if (string.IsNullOrEmpty(userId) || userId == "")
                    return false;

                System.Diagnostics.Debug.WriteLine($"Mock favorite removed: Car {carId} for User {userId}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error removing favorite: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsFavoriteAsync(int carId)
        {
            try
            {
                await Task.Delay(50); 
                
                var userId = await _secureStorage.GetAsync("user_id");
                if (string.IsNullOrEmpty(userId) || userId == "")
                    return false;

                return userId switch
                {
                    "1" => carId == 1 || carId == 3,
                    "2" => carId == 2 || carId == 4,
                    _ => false
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking favorite status: {ex.Message}");
                return false;
            }
        }
    }
}
