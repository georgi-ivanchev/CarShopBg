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
                var brand = data.Brands.Where(b => b.Id == car.BrandId).ToList();
                var model = data.Models.Where(m => m.Id == car.ModelId).ToList();

                car.Brand = brand[0].Name;
                car.Model = model[0].Name;

            }


            return cars;
        }

        public int CarsCount() => data.Cars.Count();

    }
}
