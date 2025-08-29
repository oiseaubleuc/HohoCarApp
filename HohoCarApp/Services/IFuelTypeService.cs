using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public interface IFuelTypeService
    {
        Task<List<FuelType>> GetFuelTypesAsync();
        Task<FuelType> GetFuelTypeByIdAsync(int id);
        Task AddFuelTypeAsync(FuelType fuelType);
        Task UpdateFuelTypeAsync(FuelType fuelType);
        Task DeleteFuelTypeAsync(int id);
    }
}

