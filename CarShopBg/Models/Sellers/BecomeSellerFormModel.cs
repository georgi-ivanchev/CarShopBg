namespace CarShopBg.Models.Sellers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using CarShopBg.Data.Models;

    using static Data.DataConstants;

    public class BecomeSellerFormModel
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(SellerNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLenght)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
