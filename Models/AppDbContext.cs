using Microsoft.EntityFrameworkCore;
using System;

namespace PatikaDevParamHafta1Odev.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id= 1,
                    Name="Çikolatalı Gofret",
                    CategoryName="Food",
                    Description= "40g chocolate covered wafers",
                    Quantity=100,
                    Price=5,
                    SaleStatus = false
                },
                new Product()
                {
                    Id = 2,
                    Name = "Ice Tea",
                    CategoryName = "Food",
                    Description = "500ml peach flavored ice tea",
                    Quantity = 50,
                    Price = 12,
                    SaleStatus = true
                },
                new Product()
                {
                    Id = 3,
                    Name = "Makarna",
                    CategoryName = "Food",
                    Description = "150g spaghetti",
                    Quantity = 100,
                    Price = 8,
                    SaleStatus = true
                },
                new Product()
                {
                    Id = 4,
                    Name = "Domates Salçası",
                    CategoryName = "Food",
                    Description = "80g tomato paste",
                    Quantity = 100,
                    Price = 20,
                    SaleStatus = false
                }
                );
        }
    }
}
