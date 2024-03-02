using Gate.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gate.Identity.Context
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, 
                                       IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, 
                                       IdentityRoleClaim<int>, IdentityUserToken<int>>    
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                    userRole.HasOne(ur => ur.User)
                            .WithMany(r => r.UserRoles)    
                            .HasForeignKey(ur => ur.UserId)
                            .IsRequired();

                    userRole.HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired();
                }
            );
        }
    }
}