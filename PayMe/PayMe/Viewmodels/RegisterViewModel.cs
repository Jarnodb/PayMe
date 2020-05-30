using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayMe.Viewmodels
{
    [NotMapped]
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[^,]+$",
            ErrorMessage = "Name cannot contain comma")]
        public string FirstName { get; set; }
        
        [Required]
        [RegularExpression(@"^[^,]+$",
            ErrorMessage = "Lastname cannot contain comma")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Email must be a valid Address Email.")]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }      
        public string UniqueCode { get; set; }
        public decimal Money { get; set; }
        public string RecentTransactions { get; set; }


    }
}
