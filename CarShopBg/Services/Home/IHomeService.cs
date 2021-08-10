namespace CarShopBg.Services.Home
{
    using System.Collections.Generic;
    using CarShopBg.Services.Home.Models;
    public interface IHomeService
    {
        List<LastCarsServiceModel> GetLastThreeCars();

        int CarsCount();
    }
}
