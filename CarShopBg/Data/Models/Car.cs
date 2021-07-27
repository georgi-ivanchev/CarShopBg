namespace CarShopBg.Data.Models
{
    using CarShopBg.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Car
    {
        [Required]
        public int Id { get; init; }

        public Brand Brand { get; init; }

        [Required]
        public int BrandId { get; init; }

        public Model Model { get; init; }

        [Required]
        public int ModelId { get; init; }

        [Required]
        public int Price { get; init; }

        [Required]
        public int Mileage { get; init; }

        [Required]
        public string FirstRegistration { get; init; }

        [Required]
        public Fuel FuelType { get; init; }

        [Required]
        [MaxLength(CarDescriptionMaxLenght)]
        public string Description { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        public int HorsePower { get; init; }

        public Gearbox Gearbox { get; init; }

        public string EngineCapacity { get; init; }

        public int CategoryId { get; init; }

        public Category Category { get; init; }

    }
}
