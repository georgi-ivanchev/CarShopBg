namespace CarShopBg.Services.Cars
{
    using System.ComponentModel.DataAnnotations;
    public class CarServiceModel
    {
        public int Id { get; init; }

        [Required]
        public int Price { get; init; }

        [Required]
        [Display(Name = "First Registration")]
        public string FirstRegistration { get; init; }

        public bool IsApproved { get; set; }
        public int BrandId { get; init; }

        public int ModelId { get; init; }

        public int CategoryId { get; init; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }
    }
}
