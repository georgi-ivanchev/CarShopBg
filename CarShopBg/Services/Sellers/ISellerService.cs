using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopBg.Services.Sellers
{
    public interface ISellerService
    {
        bool IsSeller(string userId);

        bool IsCarSeller(int carId, int sellerId);

        int IdByUserId(string userId);

        int IdByCarId(int carId);

        void CreateSeller(string name,
            string phoneNumber,
            string userId);
    }
}
