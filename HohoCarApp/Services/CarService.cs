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
                new Car 
                { 
                    Id = 1, 
                    Brand = "Mercedes", 
                    Model = "Classe G", 
                    Price = 125000, 
                    Year = 2023, 
                    Mileage = 15000,
                    Description = "Luxury SUV with premium features and excellent off-road capabilities. Perfect for both city driving and adventurous trips.",
                    ImageUrl = "https://images.unsplash.com/photo-1563720223185-11003d516935?w=800&h=600&fit=crop",
                    Category = "SUV",
                    FuelType = "Diesel",
                    Location = "Luik",
                    Views = 45
                },
                new Car 
                { 
                    Id = 2, 
                    Brand = "Tesla", 
                    Model = "Model 3", 
                    Price = 45000, 
                    Year = 2022, 
                    Mileage = 8000,
                    Description = "Electric vehicle with cutting-edge technology, autopilot features, and impressive range. The future of driving.",
                    ImageUrl = "https://images.unsplash.com/photo-1536700503339-1e4b06520771?w=800&h=600&fit=crop",
                    Category = "Sedan",
                    FuelType = "Electric",
                    Location = "Bruxelles",
                    Views = 32
                },
                new Car 
                { 
                    Id = 3, 
                    Brand = "BMW", 
                    Model = "X5", 
                    Price = 75000, 
                    Year = 2021, 
                    Mileage = 25000,
                    Description = "Premium SUV with sporty handling and luxurious interior. Perfect combination of performance and comfort.",
                    ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=800&h=600&fit=crop",
                    Category = "SUV",
                    FuelType = "Gasoline",
                    Location = "Anvers",
                    Views = 28
                }
            };
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            return await Task.FromResult(cars);
        }

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId);
            return await Task.FromResult(car);
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            car.Id = cars.Max(c => c.Id) + 1;
            cars.Add(car);
            return await Task.FromResult(car);
        }

        public async Task UpdateCarAsync(Car car)
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
                existingCar.Category = car.Category;
                existingCar.FuelType = car.FuelType;
                existingCar.Location = car.Location;
                existingCar.Views = car.Views;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                cars.Remove(car);
            }
            await Task.CompletedTask;
        }
    }
}
