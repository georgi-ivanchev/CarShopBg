namespace CarShopBg.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CarShopBg.Models.Sellers;
    using CarShopBg.Services.Sellers;

    public class SellersController : Controller
    {
        private readonly ISellerService seller;

        public SellersController(ISellerService seller) =>  this.seller = seller;

        [Authorize]
        public IActionResult Become() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeSellerFormModel seller)
        {
            this.seller.CreateSeller(seller.Name, seller.PhoneNumber, seller.UserId);
            return RedirectToAction(nameof(CarsController.All), "Cars");
        }

    }
}
