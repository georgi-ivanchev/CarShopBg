namespace CarShopBg.Services.Cars
{
    using System.Collections.Generic;
    using CarShopBg.Services.Cars.Models;
    public class AllCarsServiceModel
    {
        public List<CarServiceModel> Cars { get; set; }

        public IEnumerable<BrandAndCategoryServiceModel> Categories { get; set; }

        public IEnumerable<BrandAndCategoryServiceModel> Brands { get; set; }

        public IEnumerable<ModelServiceModel> Models { get; set; }
    }
}
