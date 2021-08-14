namespace CarShopBg.Services.Cars
{
    using System.Collections.Generic;
    using CarShopBg.Models.Cars;
    using CarShopBg.Models.Enums;
    using CarShopBg.Services.Cars.Models;
    public interface ICarService
    {
        AllCarsServiceModel AllCars();

        AllCarsServiceModel GetMyOffers(string userId);

        bool CreateCar
            (int brandId,
            int modelId,
            int price,
            int mileage,
            string firstRegistration,
            Fuel fuelType,
            string Description,
            string imageUrl,
            int categoryId,
            string engineCapacity,
            int horsePower,
            Gearbox gearbox,
            int sellerId);

        CarDetailsViewModel Details(int carId);

        bool DeleteCar(int carId);

        bool EditCar(int carId, CarOfferFormModel carModel);

        IEnumerable<BrandAndCategoryServiceModel> GetCarCategories();

        IEnumerable<BrandAndCategoryServiceModel> GetCarBrands();

        IEnumerable<ModelServiceModel> GetCarModels();

        
    }
}
