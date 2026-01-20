using Microsoft.AspNetCore.Identity;

namespace Simulation7.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
