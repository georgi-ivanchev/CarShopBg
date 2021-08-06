namespace CarShopBg.Services.Cars
{
    using CarShopBg.Data;
    using CarShopBg.Data.Models;
    using CarShopBg.Services.Cars.Models;
    using System.Collections.Generic;
    using System.Linq;
    using CarShopBg.Models.Enums;
    using CarShopBg.Models.Cars;

    public class CarService : ICarService
    {
        private readonly CarShopBgDbContext data;

        public CarService(CarShopBgDbContext data) => this.data = data;
        
        
        public AllCarsServiceModel AllCars()
        {
            var cars = data.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarServiceModel
                {
                    BrandId = c.BrandId,
                    ModelId = c.ModelId,
                    Price = c.Price,
                    FirstRegistration = c.FirstRegistration,
                    Id = c.Id,
                    ImageUrl = c.ImageUrl,
                    CategoryId = c.CategoryId
                })
                .ToList();

            foreach (var car in cars)
            {
                var brand = data.Brands.Where(b => b.Id == car.BrandId).ToList();
                var model = data.Models.Where(m => m.Id == car.ModelId).ToList();
                var category = data.Categories.Where(c => c.Id == car.CategoryId).ToList();

                car.Brand = brand[0].Name;
                car.Model = model[0].Name;
                car.Category = category[0].Name;
            }

            var totalCars = data.Cars.Count();

            var allCars = new AllCarsServiceModel
            {
                Cars = cars,
                Categories = GetCarCategories(),
                Brands = GetCarBrands(),
                Models = GetCarModels()
            };

            return allCars;
        }

        public void CreateCar(
            int brandId,
            int modelId,
            int price,
            int mileage,
            string firstRegistration,
            Fuel fuelType,
            string Description,
            string imageUrl,
            int categoryId,
            string engineCapacity,
            int horsePower,
            Gearbox gearbox,
            int sellerId)
        {
            var car = new Car
            {
                BrandId = brandId,
                ModelId = modelId,
                Price = price,
                Mileage = mileage,
                FirstRegistration = firstRegistration,
                FuelType = fuelType,
                Description = Description,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                EngineCapacity = engineCapacity,
                HorsePower = horsePower,
                Gearbox = gearbox,
                SellerId = sellerId
            };

            data.Cars.Add(car);
            data.SaveChanges();
        }

        public CarDetailsViewModel Details(int carId)
        {
            var car = data.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarDetailsViewModel
                {
                    Id = c.Id,
                    BrandId = c.BrandId,
                    ModelId = c.ModelId,
                    CategoryId = c.CategoryId,
                    Description = c.Description,
                    EngineCapacity = c.EngineCapacity,
                    FirstRegistration = c.FirstRegistration,
                    FuelType = c.FuelType,
                    ImageUrl = c.ImageUrl,
                    Mileage = c.Mileage,
                    Price = c.Price,
                    Gearbox = c.Gearbox,
                    HorsePower = c.HorsePower
                })
                .FirstOrDefault();

            var brand = data.Brands.Where(b => b.Id == car.BrandId).ToList();
            var model = data.Models.Where(m => m.Id == car.ModelId).ToList();
            var category = data.Categories.Where(c => c.Id == car.CategoryId).ToList();

            car.Brand = brand[0].Name;
            car.Model = model[0].Name;
            car.Category = category[0].Name;

            return car;
        }

        public IEnumerable<BrandAndCategoryServiceModel> GetCarCategories()
            => this.data
                .Categories
                .Select(c => new BrandAndCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public IEnumerable<BrandAndCategoryServiceModel> GetCarBrands()
            => this.data
                .Brands
                .Select(c => new BrandAndCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public IEnumerable<ModelServiceModel> GetCarModels()
            => this.data
                .Models
                .Select(c => new ModelServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BrandId = c.BrandId
                })
                .ToList();

    }
}
