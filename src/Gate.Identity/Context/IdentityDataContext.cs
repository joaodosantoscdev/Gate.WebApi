using Gate.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gate.Identity.Context
{
    public class IdentityDataContext : IdentityDbContext    
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { } 
    }
}