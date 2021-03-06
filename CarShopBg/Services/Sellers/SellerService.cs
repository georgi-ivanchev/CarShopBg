namespace CarShopBg.Services.Sellers
{
    using System.Linq;
    using CarShopBg.Data;
    using CarShopBg.Data.Models;

    public class SellerService : ISellerService
    {
        private readonly CarShopBgDbContext data;

        public SellerService(CarShopBgDbContext data) => this.data = data;

        public bool IsSeller(string userId)
              => this.data
            .Sellers
            .Any(s => s.UserId == userId);

        public bool IsCarSeller(int carId, int sellerId)
            => this.data
            .Cars
            .Where(c=>c.Id == carId)
            .Any(c => c.SellerId == sellerId);

        public int IdByUserId(string userId)
            => this.data
            .Sellers
            .Where(s => s.UserId == userId)
            .Select(s => s.Id)
            .FirstOrDefault();

        public int IdByCarId(int carId)
            => this.data
            .Cars
            .Where(c => c.Id == carId)
            .Select(c => c.SellerId)
            .FirstOrDefault();

        public void CreateSeller(string name,
            string phoneNumber,
            string userId)
        {
            var seller = new Seller
            {
                Name = name,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            this.data.Add(seller);
            this.data.SaveChanges();
        }

    }
}
