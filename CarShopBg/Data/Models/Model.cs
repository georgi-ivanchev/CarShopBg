namespace CarShopBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Model
    {
        [Required]
        public int Id { get; init; }

        public string Name { get; init; }

        [Required]
        public int BrandId { get; init; }

        public Brand Brand { get; init; }

        public IEnumerable<Car> Cars { get; init; }
    }
}
