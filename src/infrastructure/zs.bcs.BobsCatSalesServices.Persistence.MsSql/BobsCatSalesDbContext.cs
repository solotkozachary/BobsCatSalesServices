using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using zs.bcs.BobsCatSalesServices.Domain.Entity.EntityIdentity;
using zs.bcs.BobsCatSalesServices.Domain.Entity.SalesAssociate;
using zs.bcs.BobsCatSalesServices.Persistence.MsSql.EntityConfigurations;

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
        public DbSet<PersonalDesignation> PersonalDesignations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(x => x.EntityKey)
                ;
            modelBuilder.Entity<Email>()
                .HasKey(x => x.EntityKey)
                ;
            modelBuilder.Entity<Phone>()
                .HasKey(x => x.EntityKey)
                ;
            modelBuilder.Entity<PersonalDesignation>()
                .HasKey(x => x.EntityKey)
                ;
            modelBuilder.Entity<SalesAssociate>()
                .HasKey(x => x.EntityKey)
                ;

            modelBuilder.ApplyConfiguration(new SalesAssociateConfiguration());

            modelBuilder.Entity<SalesAssociate>()
                .HasOne(x => x.PersonalDesignation)
                .WithOne(x => x.SalesAssociate)
                .HasForeignKey<PersonalDesignation>(x => x.RelationalEntityKey)
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.Addresses)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<SalesAssociate>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.SalesAssociate)
                .HasForeignKey(e => e.RelationalEntityKey)
                .HasPrincipalKey(e => e.EntityKey)
                .OnDelete(DeleteBehavior.Cascade)
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
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase(connectionString);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("AppDb");
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
            }
        }
    }
}
