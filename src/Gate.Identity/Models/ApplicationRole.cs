using Microsoft.AspNetCore.Identity;

namespace Gate.Identity.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public List<ApplicationUserRole> UserRoles { get; set; }
    }
}