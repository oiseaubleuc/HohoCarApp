using System.Collections.Generic;
using System.Threading.Tasks;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetCarsAsync();
        Task<Car> GetCarByIdAsync(int carId);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}








//{
//    Task<List<Car>> GetCarsAsync(CancellationToken ct = default);
//    Task<Car?> GetCarByIdAsync(int id, CancellationToken ct = default);
//    Task AddCarAsync(Car car, CancellationToken ct = default);
//    Task UpdateCarAsync(Car car, CancellationToken ct = default);
//    Task DeleteCarAsync(int id, CancellationToken ct = default);
//}
 