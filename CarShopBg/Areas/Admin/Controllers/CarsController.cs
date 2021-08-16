namespace CarShopBg.Areas.Admin.Controllers
{
    using CarRentingSystem.Areas.Admin.Controllers;
    using CarShopBg.Models.Cars;
    using CarShopBg.Services.Cars;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : AdminController
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars) => this.cars = cars;

        public IActionResult All()
        {
            var allCars = new AllCarsViewModel
            {
                Cars = cars.AllCars(publicOnly: false).Cars,
                Categories = cars.AllCars(publicOnly: false).Categories,
                Brands = cars.AllCars(publicOnly: false).Brands,
                Models = cars.AllCars(publicOnly: false).Models
            };
            return View(allCars);
        }

        public IActionResult ApproveCar(int id)
        {
            this.cars.ApproveCar(id);

            return RedirectToAction(nameof(All));
        }
    }
}
