
using Microsoft.AspNetCore.Identity;

namespace BringIt.Users.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public string VehicleNumber { get; set; }
    }
}
