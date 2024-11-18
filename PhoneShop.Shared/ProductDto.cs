namespace PhoneShop.Shared;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
        
    // Foreign Key to Category
    public int CategoryId { get; set; }  // Foreign key property

    // Navigation Property to Category
    public CategoryDto Category { get; set; }  // Reference to the related Category entity
}
