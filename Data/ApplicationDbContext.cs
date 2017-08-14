using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Econometer.Data.Models;

namespace Econometer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entity = builder.Entity<Invoice>();

            entity.Property(p => p.Html).HasColumnType("text");

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Time>().ToTable("Times");
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Vat>().ToTable("Vats");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Invoice>().ToTable("Invoices");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vat> Vats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
