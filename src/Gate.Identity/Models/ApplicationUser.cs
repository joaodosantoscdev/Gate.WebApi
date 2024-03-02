using Gate.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Gate.Identity.Models
{
    public class ApplicationUser :  IdentityUser<int>, BaseInfo
    {
        public string Description { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserImage { get; set; }
        public IEnumerable<ApplicationUserRole> UserRoles { get; set; }
        public int CreatedUser { get; set; }
        public int UpdatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}