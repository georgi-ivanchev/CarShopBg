namespace CarShopBg.Controllers
{
    using System.Security.Claims;
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.seller.CreateSeller(seller.Name, seller.PhoneNumber, userId);
            return RedirectToAction(nameof(CarsController.All), "Cars");
        }

    }
}
