using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopBg.Services.Cars.Models
{
    public class ModelServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int BrandId { get; set; }
    }
}
