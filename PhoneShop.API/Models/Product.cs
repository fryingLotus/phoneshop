namespace PhoneShop.API.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
        
    // Foreign Key to Category
    public int CategoryId { get; set; }  // Foreign key property

    // Navigation Property to Category
    public Category Category { get; set; }  // Reference to the related Category entity
}