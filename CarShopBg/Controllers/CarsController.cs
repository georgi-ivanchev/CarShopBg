namespace CarShopBg.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CarShopBg.Models.Cars;
    using CarShopBg.Services.Cars;
    using CarShopBg.Infrastructure;
    using CarShopBg.Services.Sellers;

    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly ISellerService sellers;
        public CarsController(ICarService cars, ISellerService sellers)
        {
            this.cars = cars;
            this.sellers = sellers;
        }



        [Display(Name = "Add New Offer")]
        [Authorize]
        public IActionResult Create() => View(new CreateCarOfferFormModel
        {
            Categories = cars.GetCarCategories(),
            Brands = cars.GetCarBrands(),
            Models = cars.GetCarModels()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateCarOfferFormModel carModel)
        {
            var userId = this.User.Id();
            if (!sellers.IsSeller(userId))
            {
                return Redirect("/");
            }
            var sellerId = sellers.IdByUserId(userId);
            if (!ModelState.IsValid)
            {
                carModel.Categories = cars.GetCarCategories();
                carModel.Brands = cars.GetCarBrands();
                carModel.Models = cars.GetCarModels();
                return View(carModel);
            }

            cars.CreateCar(
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
                carModel.Gearbox,
                sellerId);

            return Redirect("/");
        }

        public IActionResult All()
        {
            var allCars = new AllCarsViewModel
            {
                Cars = cars.AllCars().Cars,
                Categories = cars.AllCars().Categories,
                Brands = cars.AllCars().Brands,
                Models = cars.AllCars().Models
            };
            cars.AllCars();

            return View(allCars);
        }

        public IActionResult Details(int carId)
        {
            var userId = this.User.Id();

            var car = cars.Details(carId);

            return View(car);
        }
    }
}
