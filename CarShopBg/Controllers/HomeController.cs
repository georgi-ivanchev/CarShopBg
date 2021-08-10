namespace CarShopBg.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using CarShopBg.Models;
    using CarShopBg.Models.Home;
    using CarShopBg.Services.Home;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService) => this.homeService = homeService;

        public IActionResult Index()
        {
            var cars = homeService.GetLastThreeCars();
            var totalCars = homeService.CarsCount();

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
