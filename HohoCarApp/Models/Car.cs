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

            public string Brand { get; set; }

            public string Model { get; set; }

            public decimal Price { get; set; }

            public int Year { get; set; }

            public string Description { get; set; }

            public string ImageUrl { get; set; }

            public int Mileage { get; set; }

            public string Category { get; set; } = "SUV";

            public string FuelType { get; set; } = "Diesel";

            public string Location { get; set; } = "Luik";

            public int Views { get; set; } = 0;
        }
    


}