using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity;
using zs.bcs.BobsCatSalesServices.Domain.Entity.SalesAssociate;

namespace zs.bcs.BobsCatSalesServices.Persistence.MsSql
{
    /// <summary>
    /// The EF database context for Bob's Cat Sales entities.
    /// </summary>
    public class BobsCatSalesDbContext : DbContext
    {
        public BobsCatSalesDbContext(DbContextOptions<BobsCatSalesDbContext> options) : base(options) { }

        public DbSet<SalesAssociate> SalesAssociates { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Address> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                ;

            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.Addresses)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                ;

            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                ;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (environment == "Development")
            {
                var connectionString = configuration.GetConnectionString("InMemAppDb");
                optionsBuilder.UseInMemoryDatabase(connectionString);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("AppDb");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
