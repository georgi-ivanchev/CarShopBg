using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopBg.Models.Cars
{
    public class CarModelViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int BrandId { get; set; }
    }
}
