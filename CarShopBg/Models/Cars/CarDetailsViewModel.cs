namespace CarShopBg.Models.Cars
{
    using CarShopBg.Models.Enums;
    public class CarDetailsViewModel
    {
    
        public int Id { get; init; }

        public string Brand { get; set; }

        public int BrandId { get; init; }

        public string Model { get; set; }

        public int ModelId { get; init; }

        public int Price { get; init; }

        public int Mileage { get; init; }

        public string FirstRegistration { get; init; }

        public Fuel FuelType { get; init; }

        public Gearbox Gearbox { get; init; }

        public int HorsePower { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public string EngineCapacity { get; init; }

        public int CategoryId { get; init; }

        public string Category { get; set; }

        public string SellerName { get; init; }

        public string SellerPhoneNumber { get; init; }

    }
}

