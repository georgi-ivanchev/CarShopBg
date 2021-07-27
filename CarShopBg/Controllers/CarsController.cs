namespace CarShopBg.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using System.ComponentModel.DataAnnotations;
    using CarShopBg.Data;
    using CarShopBg.Data.Models;
    using CarShopBg.Models.Cars;
    using CarShopBg.Models.Home;

    public class CarsController : Controller
    {
        private readonly CarShopBgDbContext data;

        public CarsController(CarShopBgDbContext data)
            => this.data = data;



        [Display(Name = "Add New Offer")]
        [Authorize]
        public IActionResult Create() => View(new CreateCarOfferFormModel
        {
            Categories = GetCarCategories(),
            Brands = GetCarBrands(),
            Models = GetCarModels()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateCarOfferFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Categories = GetCarCategories();
                carModel.Brands = GetCarBrands();
                carModel.Models = GetCarModels();

                return View(carModel);
            }
            var car = new Car
            {
                BrandId = carModel.BrandId,
                ModelId = carModel.ModelId,
                Price = carModel.Price,
                Mileage = carModel.Mileage,
                FirstRegistration = carModel.FirstRegistration,
                FuelType = carModel.FuelType,
                Description = carModel.Description,
                ImageUrl = carModel.ImageUrl,
                CategoryId = carModel.CategoryId,
                EngineCapacity = carModel.EngineCapacity,
                HorsePower = carModel.HorsePower,
                Gearbox = carModel.Gearbox
            };
            data.Cars.Add(car);
            data.SaveChanges();

            return Redirect("/");
        }

        public IActionResult All()
        {
            var cars = data.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarIndexViewModel
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

            return View(new AllCarsViewModel
            {
                Cars = cars,
                Categories = GetCarCategories(),
                Brands = GetCarBrands(),
                Models = GetCarModels()
            });
        }

        public IActionResult Details(int carId)
        {
            var car = data.Cars
                .Where(c => c.Id == carId)
                .Select(c=> new CarDetailsViewModel
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
            
            return View(car);
        }

        private IEnumerable<CarCategoryViewModel> GetCarCategories()
            => this.data
                .Categories
                .Select(c => new CarCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<CarBrandViewModel> GetCarBrands()
            => this.data
                .Brands
                .Select(c => new CarBrandViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<CarModelViewModel> GetCarModels()
            => this.data
                .Models
                .Select(c => new CarModelViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BrandId = c.BrandId
                })
                .ToList();

    }
}
