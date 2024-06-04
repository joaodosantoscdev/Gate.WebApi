using Gate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ResidentInfo> Residents { get; set; }
        public DbSet<UnitInfo> Units { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<ComplexInfo> Complexes { get; set; }
        public DbSet<AccessInfo> Access { get; set; }
        public DbSet<PlaceInfo> Places { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResidentInfo>().ToTable("RESIDENTS");
            modelBuilder.Entity<UnitInfo>().ToTable("UNITIES");
            modelBuilder.Entity<ContactInfo>().ToTable("CONTACTS");
            modelBuilder.Entity<ComplexInfo>().ToTable("COMPLEXES");
            modelBuilder.Entity<AccessInfo>().ToTable("ACCESSES");
            modelBuilder.Entity<PlaceInfo>().ToTable("PLACES");

            modelBuilder.Entity<AccessInfo>(access => 
                {
                    access.HasKey(a => new { a.Id });

                    access.HasOne(a => a.Resident)
                        .WithMany(r => r.Accesses)
                        .HasForeignKey(a => a.ResidentId)
                        .OnDelete(DeleteBehavior.Restrict);

                    access.HasOne(a => a.Place)
                        .WithMany(r => r.Accesses)
                        .HasForeignKey(a => a.PlaceId)
                        .OnDelete(DeleteBehavior.Restrict);
                }
            );

            modelBuilder.Entity<UnitInfo>(unit => 
                {
                    unit.HasKey(u => u.Id);

                    unit.HasOne(u => u.Complex)
                        .WithMany(c => c.Unities)
                        .HasForeignKey(u => u.ComplexId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity<PlaceInfo>(place => 
                { 
                    place.HasKey(u => u.Id);
                    
                    place.HasOne(p => p.Unit)
                        .WithMany(u => u.Places)
                        .HasForeignKey(p => p.UnitId)
                        .OnDelete(DeleteBehavior.Cascade);

                    place.HasOne(p => p.Resident)
                        .WithMany(r => r.Places)
                        .HasForeignKey(p => p.ResidentId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity<ContactInfo>(contact =>
                {
                    contact.HasKey(c => c.Id);

                    contact.HasOne(c => c.Resident)
                            .WithMany(p => p.Contacts)
                            .HasForeignKey(c => c.ResidentId);

                    contact.Ignore(c => c.Resident);
                }
            );

            modelBuilder.Entity<ResidentInfo>(contact =>
                {
                    contact.HasKey(r => r.Id);
                }
            );
        }
    }
}