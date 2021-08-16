namespace CarShopBg.Services.Home
{
    using System.Linq;
    using System.Collections.Generic;
    using CarShopBg.Data;
    using CarShopBg.Services.Home.Models;
    public class HomeService : IHomeService
    {
        private readonly CarShopBgDbContext data;

        public HomeService(CarShopBgDbContext data) => this.data = data;

        public List<LastCarsServiceModel> GetLastThreeCars()
        {
            var cars = data.Cars
                .OrderByDescending(c => c.Id)
                .Where(c => c.IsApproved == true)
                .Select(c => new LastCarsServiceModel
                {
                    BrandId = c.BrandId,
                    ModelId = c.ModelId,
                    Price = c.Price,
                    FirstRegistration = c.FirstRegistration,
                    Id = c.Id,
                    ImageUrl = c.ImageUrl
                })
                .Take(3)
                .ToList();

            foreach (var car in cars)
            {
                var brand = data.Brands.Find(car.BrandId);
                var model = data.Models.Find(car.ModelId);

                car.Brand = brand.Name;
                car.Model = model.Name;
            }
            return cars;
        }

        public int CarsCount() => data.Cars.Count();
    }
}
