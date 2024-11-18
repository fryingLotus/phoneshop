using Microsoft.EntityFrameworkCore;
using PhoneShop.API.Models;
using Microsoft.Extensions.Logging;
using PhoneShop.API.Data;

namespace PhoneShop.API
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, ILogger<SeedData> logger)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if products already exist
            if (context.Products.Any())
            {
                logger.LogInformation("Database already seeded.");
                return; // DB has been seeded
            }

            // Add sample categories
            var electronicsCategory = new Category { Name = "Electronics" };
            var smartphonesCategory = new Category { Name = "Smartphones" };
            context.Categories.AddRange(electronicsCategory, smartphonesCategory);
            context.SaveChanges();  // Save categories to get their IDs

            // Add sample products with valid ImageUrl
            context.Products.AddRange(
                new Product
                {
                    Name = "iPhone 13",
                    Price = 799.99m,
                    Description = "Apple iPhone 13 with 128GB storage",
                    CategoryId = smartphonesCategory.Id,  // Reference the CategoryId
                    ImageUrl = "https://example.com/images/iphone13.jpg" // Add a valid ImageUrl
                },
                new Product
                {
                    Name = "Samsung Galaxy S21",
                    Price = 799.99m,
                    Description = "Samsung Galaxy S21 with 128GB storage",
                    CategoryId = smartphonesCategory.Id,  // Reference the CategoryId
                    ImageUrl = "https://example.com/images/galaxy_s21.jpg" // Add a valid ImageUrl
                },
                new Product
                {
                    Name = "MacBook Pro 16-inch",
                    Price = 2399.99m,
                    Description = "Apple MacBook Pro with M1 chip, 16GB RAM",
                    CategoryId = electronicsCategory.Id,  // Reference the CategoryId
                    ImageUrl = "https://example.com/images/macbook_pro.jpg" // Add a valid ImageUrl
                }
            );

            // Save changes to the database
            context.SaveChanges();
            logger.LogInformation("Sample products and categories have been added to the database.");
        }
    }
}
