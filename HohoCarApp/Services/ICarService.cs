using System.Collections.Generic;
using System.Threading.Tasks;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetCarsAsync();
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}
