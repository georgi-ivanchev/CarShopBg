namespace CarShopBg.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Brand
    {
        [Required]
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<Car> Cars { get; init; }

        public IEnumerable<Model> Models { get; init; }
    }
}
