using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HohoCarApp.Models;

namespace HohoCarApp.Services
{
    public class CarService : ICarService
    {
        private readonly List<Car> cars;

        public CarService()
        {
            cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Price = 20000, Year = 2022, Mileage = 5000 },
                new Car { Id = 2, Brand = "Tesla", Model = "Model 3", Price = 35000, Year = 2021, Mileage = 2000 }
            };
        }

        public Car GetCarById(int id)
        {
            return cars.FirstOrDefault(c => c.Id == id);
        }

        public Task<List<Car>> GetCarsAsync()
        {
            return Task.FromResult(cars);
        }

        public Task AddCarAsync(Car car)
        {
            car.Id = cars.Max(c => c.Id) + 1;
            cars.Add(car);
            return Task.CompletedTask;
        }

        public Task UpdateCarAsync(Car car)
        {
            var existingCar = cars.FirstOrDefault(c => c.Id == car.Id);
            if (existingCar != null)
            {
                existingCar.Brand = car.Brand;
                existingCar.Model = car.Model;
                existingCar.Price = car.Price;
                existingCar.Year = car.Year;
                existingCar.Mileage = car.Mileage;
                existingCar.Description = car.Description;
                existingCar.ImageUrl = car.ImageUrl;
            }
            return Task.CompletedTask;
        }

        public Task DeleteCarAsync(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                cars.Remove(car);
            }
            return Task.CompletedTask;
        }
    }
}
