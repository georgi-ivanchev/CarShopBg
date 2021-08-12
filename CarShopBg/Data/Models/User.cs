namespace CarShopBg.Data.Models
{

    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class User : IdentityUser
    {
        [MaxLength(UserFirstNameMaxLenght)]
        public string FirstName { get; init; }

        [MaxLength(UserLastNameMaxLenght)]
        public string LastName { get; init; }
    }
}
