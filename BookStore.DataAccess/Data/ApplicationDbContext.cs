using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
               
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Ctg_ID = 1 , Name = "Action" , DisplayOrder=1},
                new Category { Ctg_ID = 2, Name = "SCI-FI", DisplayOrder = 2 },
                new Category { Ctg_ID = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product 
                {
                    ProdID = 1,
                    Title = "Fortune of Time",
                    Author = "Mutiullah",
                    Description = "Description",
                    ISBN = "swd110",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryID = 1,
                    ImageURL=""
                
                },
                new Product
                {
                    ProdID = 2,
                    Title = "UnFortune of Time",
                    Author = "Mutiullah Yousfani",
                    Description = "Description1",
                    ISBN = "swd111",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryID=2,
                    ImageURL=""
                }
                );
        }

    }
}
