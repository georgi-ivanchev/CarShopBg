using System;
namespace CarShopBg.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarShopBg.Models.Enums;
    using CarShopBg.Services.Cars.Models;
    using static Data.DataConstants;
    public class CreateCarOfferFormModel
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public int Price { get; init; }

        [Required]
        public int Mileage { get; init; }

        [Required]
        [Display(Name = "First Registration")]
        public string FirstRegistration { get; init; }

        [Required]
        [Display(Name = "Fuel Type")]
        [EnumDataType(typeof(Fuel))]
        public Fuel FuelType { get; init; }

        [Required]
        [EnumDataType(typeof(Gearbox))]
        public Gearbox Gearbox { get; init; }

        [Required]
        public int HorsePower { get; init; }

        [Required]
        [MaxLength(CarDescriptionMaxLenght)]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Engine Capacity")]
        public string EngineCapacity { get; init; }

        [Display(Name = "Brand")]
        public int BrandId { get; init; }

        [Display(Name = "Model")]
        public int ModelId { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [Required]
        public int SellerId { get; init; }



        public IEnumerable<BrandAndCategoryServiceModel> Categories { get; set; }

        public IEnumerable<BrandAndCategoryServiceModel> Brands { get; set; }

        public IEnumerable<ModelServiceModel> Models { get; set; }
    }
}