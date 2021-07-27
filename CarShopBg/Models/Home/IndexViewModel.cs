namespace CarShopBg.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalCars { get; init; }

        public List<CarIndexViewModel> Cars { get; set; }
    }
}
