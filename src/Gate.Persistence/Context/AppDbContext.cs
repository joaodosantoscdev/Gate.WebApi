using Gate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ResidentInfo> Residents { get; set; }
        public DbSet<UnitInfo> Units { get; set; }
        public DbSet<UnitResidentInfo> UnitResidents { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<ComplexInfo> Complexes { get; set; }
        public DbSet<GuestInfo> Guests { get; set; }
        public DbSet<AccessInfo> Access { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResidentInfo>().ToTable("RESIDENTS");
            modelBuilder.Entity<UnitInfo>().ToTable("UNITIES");
            modelBuilder.Entity<UnitResidentInfo>().ToTable("UNITRESIDENTS");
            modelBuilder.Entity<ContactInfo>().ToTable("CONTACTS");
            modelBuilder.Entity<ComplexInfo>().ToTable("COMPLEXES");
            modelBuilder.Entity<GuestInfo>().ToTable("GUESTS");
            modelBuilder.Entity<AccessInfo>().ToTable("ACCESSES");

            

            modelBuilder.Entity<UnitResidentInfo>(unitResident => 
                {
                    unitResident.HasKey(ur => new { ur.UnitId, ur.ResidentId });

                    unitResident.HasOne(ur => ur.Resident)
                                .WithMany()    
                                .IsRequired();

                    unitResident.HasOne(ur => ur.Unit)
                                .WithMany()    
                                .IsRequired();
                }
            );

            modelBuilder.Entity<AccessInfo>(access => 
                {
                    access.HasKey(a => new { a.PersonId, a.UnitId });

                    access.HasOne(a => a.Person)
                          .WithMany(p => p.Accesses)
                          .HasForeignKey(a => a.PersonId);

                    access.HasOne(a => a.Unit)
                           .WithMany(u => u.Accesses)
                           .HasForeignKey(a => a.UnitId);
                }
            );

            modelBuilder.Entity<UnitInfo>(unit => 
                {
                    unit.HasKey(u => u.ComplexId);

                    unit.HasOne(u => u.Complex)
                        .WithMany(c => c.Unities)
                        .HasForeignKey(u => u.ComplexId);
                }
            );

            modelBuilder.Entity<ContactInfo>(contact =>
                {
                    contact.HasKey(c => c.PersonId);

                    contact.HasOne(c => c.Person)
                            .WithMany(p => p.Contacts)
                            .HasForeignKey(c => c.PersonId);

                    contact.Ignore(c => c.Person);
                }
            );
        }
    }
}