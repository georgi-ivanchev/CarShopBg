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
    using CarShopBg.Services.Cars;
    using CarShopBg.Services.Cars.Models;

    public class CarsController : Controller
    {
        private readonly CarShopBgDbContext data;
        private readonly ICarService carService;

        public CarsController(CarShopBgDbContext data, ICarService carService)
        {
            this.data = data;
            this.carService = carService;
        }



        [Display(Name = "Add New Offer")]
        [Authorize]
        public IActionResult Create() => View(new CreateCarOfferFormModel
        {
            Categories = carService.GetCarCategories(),
            Brands = carService.GetCarBrands(),
            Models = carService.GetCarModels()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateCarOfferFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Categories = carService.GetCarCategories();
                carModel.Brands = carService.GetCarBrands();
                carModel.Models = carService.GetCarModels();
                return View(carModel);
            }
            carService.CreateCar(
                carModel.BrandId,
                carModel.ModelId,
                carModel.Price,
                carModel.Mileage,
                carModel.FirstRegistration,
                carModel.FuelType,
                carModel.Description,
                carModel.ImageUrl,
                carModel.CategoryId,
                carModel.EngineCapacity,
                carModel.HorsePower,
                carModel.Gearbox);

            return Redirect("/");
        }

        public IActionResult All()
        {
            var allCars = new AllCarsViewModel
            {
                Cars = carService.AllCars().Cars,
                Categories = carService.AllCars().Categories,
                Brands = carService.AllCars().Brands,
                Models = carService.AllCars().Models
            };
            carService.AllCars();

            return View(allCars);
        }

        public IActionResult Details(int carId)
        {
            var car = carService.Details(carId);

            return View(car);
        }
    }
}
