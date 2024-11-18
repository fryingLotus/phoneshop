using Microsoft.EntityFrameworkCore;
using PhoneShop.API.Models; // Adjust namespace if needed

namespace PhoneShop.API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; } 
    }
}