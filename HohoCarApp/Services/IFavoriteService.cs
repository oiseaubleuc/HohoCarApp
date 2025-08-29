using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetUserFavoritesAsync();
        Task<Favorite> AddFavoriteAsync(int carId);
        Task<bool> RemoveFavoriteAsync(int carId);
        Task<bool> IsFavoriteAsync(int carId);
    }
}
