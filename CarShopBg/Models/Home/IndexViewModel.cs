namespace CarShopBg.Models.Home
{
    using System.Collections.Generic;
    using CarShopBg.Services.Home.Models;

    public class IndexViewModel
    {
        public int TotalCars { get; init; }

        public List<LastCarsServiceModel> Cars { get; set; }
    }
}
