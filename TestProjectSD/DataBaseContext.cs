using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Models;

namespace TestProjectSD
{
    public class DataBaseContext: DbContext
    {
      
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Auth>  Auth { get; set; }

        public DataBaseContext(DbContextOptions options) : base(options)
        {
           // Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .HasKey(u => u.CustomerNumber);

            modelBuilder.Entity<Customer>()
           .HasMany(e => e.BusinessLocations)
           .WithOne(e => e.Customer)
           .HasForeignKey(e => e.CustomerNumber)
           .IsRequired();

            modelBuilder.Entity<BusinessLocation>()
            .HasOne(e => e.Customer)
            .WithMany(e => e.BusinessLocations)
            .HasForeignKey(e => e.CustomerNumber)
            .IsRequired();

            modelBuilder.Entity<Customer>()
                 .HasMany(e=>e.BusinessLocations);

            modelBuilder.Entity<BusinessLocation>()
                 .HasKey(u => u.Id);

            modelBuilder.Entity<Employee>()
            .HasKey(u => u.Id);

            modelBuilder.Entity<Employee>()
            .HasMany(e => e.BusinessLocations)
            .WithMany(e => e.Employees)
            .UsingEntity<EmployeeBusnessLocation>(
            l => l.HasOne<BusinessLocation>().WithMany().HasForeignKey(e => e.BusinessLocationId),
            r => r.HasOne<Employee>().WithMany().HasForeignKey(e => e.EmployeeId)); 

            modelBuilder.Entity<Auth>()
                .HasKey(u=>u.RowId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
