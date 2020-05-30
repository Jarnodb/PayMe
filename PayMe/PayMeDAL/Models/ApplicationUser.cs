using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayMeDAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal Money { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UniqueCode { get; set; }


    }
}