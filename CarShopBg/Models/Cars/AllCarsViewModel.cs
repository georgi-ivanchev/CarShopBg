namespace CarShopBg.Models.Cars
{
    using System.Collections.Generic;
    using CarShopBg.Models.Home;
    public class AllCarsViewModel
    {
        public List<CarIndexViewModel> Cars { get;set; }

        public IEnumerable<CarCategoryViewModel> Categories { get; set; }

        public IEnumerable<CarBrandViewModel> Brands { get; set; }

        public IEnumerable<CarModelViewModel> Models { get; set; }
    }
}
