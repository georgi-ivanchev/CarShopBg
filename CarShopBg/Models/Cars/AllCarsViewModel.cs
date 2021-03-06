namespace CarShopBg.Models.Cars
{
    using System.Collections.Generic;
    using CarShopBg.Services.Cars;
    using CarShopBg.Services.Cars.Models;
    public class AllCarsViewModel
    {
        public List<CarServiceModel> Cars { get; set; }

        public IEnumerable<BrandAndCategoryServiceModel> Categories { get; set; }

        public IEnumerable<BrandAndCategoryServiceModel> Brands { get; set; }

        public IEnumerable<ModelServiceModel> Models { get; set; }
    }
}
