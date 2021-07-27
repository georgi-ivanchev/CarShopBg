namespace CarShopBg.Controllers
{
    using CarShopBg.Models;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using CarShopBg.Data;
    using System.Linq;
    using CarShopBg.Models.Home;

    public class HomeController : Controller
    {
        CarShopBgDbContext data;

        public HomeController(CarShopBgDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
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

            var totalCars = data.Cars.Count();

            return View(new IndexViewModel
            {
                Cars = cars,
                TotalCars = totalCars
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
