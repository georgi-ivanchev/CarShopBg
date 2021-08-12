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
        public IActionResult Create()
        {
            var userId = this.User.Id();
            var sellerId = sellers.IdByUserId(userId);
            if (!sellers.IsSeller(userId))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }

            var model = new CarOfferFormModel
            {
                Categories = cars.GetCarCategories(),
                Brands = cars.GetCarBrands(),
                Models = cars.GetCarModels()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CarOfferFormModel carModel)
        {
            var userId = this.User.Id();
            var sellerId = sellers.IdByUserId(userId);
            if (!sellers.IsSeller(userId))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }
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

            return RedirectToAction(nameof(CarsController.All), "Cars");
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

            return View(allCars);
        }

        public IActionResult Details(int id)
        {
            var car = cars.Details(id);

            return View(car);
        }

        public IActionResult MyOffers()
        {
            var myCars = cars.GetMyOffers(User.Id());
            var allCars = new AllCarsViewModel
            {
                Cars = myCars.Cars,
                Categories = myCars.Categories,
                Brands = myCars.Brands,
                Models = myCars.Models
            };
            return View(allCars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.Id();
            var sellerId = sellers.IdByUserId(userId);
            if (!this.sellers.IsSeller(userId))
            {
                return RedirectToAction(nameof(SellersController.Become), "Sellers");
            }
            if (!sellers.IsCarSeller(id, sellerId))
            {
                return Unauthorized();
            }


            var car = cars.Details(id);

            var carForm = new CarOfferFormModel
            {
                Id = id,
                BrandId = car.BrandId,
                ModelId = car.ModelId,
                Price = car.Price,
                Mileage = car.Mileage,
                FirstRegistration = car.FirstRegistration,
                FuelType = car.FuelType,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                CategoryId = car.CategoryId,
                EngineCapacity = car.EngineCapacity,
                HorsePower = car.HorsePower,
                Gearbox = car.Gearbox,
                SellerId = sellerId,
                Brands = cars.GetCarBrands(),
                Categories = cars.GetCarCategories(),
                Models = cars.GetCarModels()
            };

            return View(carForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, CarOfferFormModel car)
        {
            var hasEdited = cars.EditCar(id, car);
            if (!hasEdited)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                car.Categories = cars.GetCarCategories();
                car.Brands = cars.GetCarBrands();
                car.Models = cars.GetCarModels();
                return View(car);
            }
            var sellerId = sellers.IdByUserId(this.User.Id());
            if (!sellers.IsCarSeller(id, sellerId))
            {
                return Unauthorized();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            var car = cars.Details(id);

            return View(car);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var sellerId = sellers.IdByUserId(this.User.Id());
            if (!sellers.IsCarSeller(id, sellerId))
            {
                return Unauthorized();
            }
            cars.DeleteCar(id);
            var hasBeenDeleted = cars.DeleteCar(id);
            if (!hasBeenDeleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(CarsController.MyOffers), "Cars");
        }
    }
}
