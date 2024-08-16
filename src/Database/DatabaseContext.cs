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
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Order> Order { get; set; }


        // static DatabaseContext()
        // {
        //     AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        //     AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        // }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }


        // can be optional
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            // .EnableSensitiveDataLogging()
            // .EnableDetailedErrors()
            .UseSnakeCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // enum => save as number 
            modelBuilder.HasPostgresEnum<Role>();

            // fluent api ????? => remove 
            // one to many : Product - Category
            //     modelBuilder.Entity<Product>()
            //         .HasOne(p => p.Category)
            //         .WithMany(c => c.Products)
            //         .HasForeignKey(p => p.CategoryId)
            //         .OnDelete(DeleteBehavior.Cascade);

            // way 1: use email as id
            // way 2: change user entity by adding [Key]

            // modelBuilder.Entity<User>()
            //   .HasKey(u => u.Email);

            // want to add seed data 
            // check if database empty
            // modelBuilder..Entity<Product>().HasData(
            //     new Product { Id = Guid.NewGuid(), Name = "p2", Description = "test", Price = 12.2, Inventory = 2, CategoryId = Guid.Parse("dcee6254-9093-401a-8394-77d2dbc9f922") }
            // );

        }

    }
}