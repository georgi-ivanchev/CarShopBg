namespace CarShopBg.Services.Cars
{
    using CarShopBg.Data;
    using CarShopBg.Data.Models;
    using CarShopBg.Services.Cars.Models;
    using System.Collections.Generic;
    using System.Linq;
    using CarShopBg.Models.Enums;
    using CarShopBg.Models.Cars;
    using CarShopBg.Services.Sellers;

    public class CarService : ICarService
    {
        private readonly CarShopBgDbContext data;
        private readonly ISellerService sellers;

        public CarService(CarShopBgDbContext data, ISellerService sellers)
        {
            this.data = data;
            this.sellers = sellers;
        }


        public AllCarsServiceModel AllCars()
        {
            var cars = GetCars(data.Cars
                .OrderByDescending(c => c.Id));
                
            return MapCarBrands(cars);
        }

        public AllCarsServiceModel GetMyOffers(string userId)
        {
            var sellerId = sellers.IdByUserId(userId);
            var cars = GetCars(this.data.Cars
                .OrderByDescending(c => c.Id)
                .Where(c => c.SellerId == sellerId));

            return MapCarBrands(cars);
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

        public bool EditCar(int carId, CarOfferFormModel carModel)
        {
            var car = data.Cars.Where(c => c.Id == carId).FirstOrDefault();
            if (car == null)
            {
                return false;
            }

            car.HorsePower = carModel.HorsePower;
            car.Price = carModel.Price;
            car.Mileage = carModel.Mileage;
            car.FirstRegistration = carModel.FirstRegistration;
            car.FuelType = carModel.FuelType;
            car.Description = carModel.Description;
            car.ImageUrl = carModel.ImageUrl;
            car.Gearbox = carModel.Gearbox;
            car.EngineCapacity = carModel.EngineCapacity;
            car.BrandId = carModel.BrandId;
            car.ModelId = carModel.ModelId;
            car.CategoryId = carModel.CategoryId;

            data.SaveChanges();
            return true;
        }

        public CarDetailsViewModel Details(int id)
        {
            var sellerId = sellers.IdByCarId(id);
            var seller = GetSeller(sellerId);
            var sellerName = seller.Name;
            var sellerPhoneNumber = seller.PhoneNumber;

            var carModel = data.Cars
                .Where(c => c.Id == id)
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
                    HorsePower = c.HorsePower,
                    SellerName = sellerName,
                    SellerPhoneNumber = sellerPhoneNumber
                })
                .FirstOrDefault();

            var brand = data.Brands.Where(b => b.Id == carModel.BrandId).ToList();
            var model = data.Models.Where(m => m.Id == carModel.ModelId).ToList();
            var category = data.Categories.Where(c => c.Id == carModel.CategoryId).ToList();

            carModel.Brand = brand[0].Name;
            carModel.Model = model[0].Name;
            carModel.Category = category[0].Name;

            return carModel;
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


        public Seller GetSeller(int sellerId)
            => this.data.Sellers.Where(s => s.Id == sellerId).FirstOrDefault();

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

        public List<CarServiceModel> GetCars(IQueryable<Car> cars)
        {
            return cars.Select(c => new CarServiceModel
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
        }

        public AllCarsServiceModel MapCarBrands(List<CarServiceModel> cars)
        {
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
    }
}
