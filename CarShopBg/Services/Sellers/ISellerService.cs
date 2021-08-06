﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopBg.Services.Sellers
{
    public interface ISellerService
    {
        bool IsSeller(string userId);

        int IdByUserId(string userId);

        void CreateSeller(string name,
            string phoneNumber,
            string userId);
    }
}
