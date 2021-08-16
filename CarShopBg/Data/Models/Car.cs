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

        [Required]
        public int Price { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string FirstRegistration { get; set; }

        [Required]
        public Fuel FuelType { get; set; }

        [Required]
        [MaxLength(CarDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int HorsePower { get; set; }

        public Gearbox Gearbox { get; set; }

        public string EngineCapacity { get; set; }

        public bool IsApproved { get; set; }

        public Brand Brand { get; init; }

        [Required]
        public int BrandId { get; set; }

        public Model Model { get; set; }

        [Required]
        public int ModelId { get; set; }

        public Category Category { get; init; }

        public int CategoryId { get; set; }

        public Seller Seller { get; init; }

        [Required]
        public int SellerId { get; set; }

    }
}
