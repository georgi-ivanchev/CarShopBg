namespace CarShopBg.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CarShopBg.Models.Sellers;
    using CarShopBg.Services.Sellers;
    using CarShopBg.Infrastructure;

    public class SellersController : Controller
    {
        private readonly ISellerService sellers;

        public SellersController(ISellerService sellers) =>  this.sellers = sellers;

        [Authorize]
        public IActionResult Become() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            var userId = this.User.Id();
            this.sellers.CreateSeller(seller.Name, seller.PhoneNumber, userId);
            return RedirectToAction(nameof(CarsController.All), "Cars");
        }

    }
}
