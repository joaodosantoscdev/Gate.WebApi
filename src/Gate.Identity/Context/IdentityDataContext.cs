using Gate.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gate.Identity.Context
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>    
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}