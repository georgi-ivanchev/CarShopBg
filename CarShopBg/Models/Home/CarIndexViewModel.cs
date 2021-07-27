namespace CarShopBg.Models.Home
{
    using System.ComponentModel.DataAnnotations;
    public class CarIndexViewModel
    {
        public int Id { get; init; }

        [Required]
        public int Price { get; init; }

        [Required]
        [Display(Name = "First Registration")]
        public string FirstRegistration { get; init; }

        public int BrandId { get; init; }

        public int ModelId { get; init; }

        public int CategoryId { get; init; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ImageUrl { get; set; }
    }
}
