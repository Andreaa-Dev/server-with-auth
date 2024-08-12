using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.src.Entity;

namespace Backend.src.Database
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration _config;

        public DbSet<User> Users { get; set; }

        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .UseSnakeCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            // one to many : Product - Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}