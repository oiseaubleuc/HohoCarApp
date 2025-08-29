using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace HohoCarApp.Models
{
    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Year { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Mileage { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; } = "SUV";

        public int FuelTypeId { get; set; }
        public string FuelType { get; set; } = "Diesel";

        public string Location { get; set; } = "Luik";

        public int Views { get; set; } = 0;

        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Car()
        {
            Brand = string.Empty;
            Model = string.Empty;
            Description = string.Empty;
            ImageUrl = string.Empty;
            Category = "SUV";
            FuelType = "Diesel";
            Location = "Luik";
        }
    }
}