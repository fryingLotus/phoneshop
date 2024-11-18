// File: PhoneShop.API/Controllers/ProductsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.API.Data;
using PhoneShop.API.Models;
using PhoneShop.Shared;

namespace PhoneShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductDto>>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .ToListAsync();

        if (products == null || !products.Any())
        {
            return NotFound(new ApiResponse<IEnumerable<ProductDto>>(null, false, "No products found"));
        }

        return Ok(new ApiResponse<IEnumerable<ProductDto>>(products, true, "Products fetched successfully"));
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetProductById(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
            .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound(new ApiResponse<ProductDto>(null, false, "Product not found"));
        }

        return Ok(new ApiResponse<ProductDto>(product, true, "Product fetched successfully"));
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            ImageUrl = productDto.ImageUrl,
            CategoryId = productDto.Category.Id
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        productDto.Id = product.Id;  // Update the DTO with the new product ID
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, new ApiResponse<ProductDto>(productDto, true, "Product created successfully"));
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, ProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(new ApiResponse<ProductDto>(null, false, "Product not found"));
        }

        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Description = productDto.Description;
        product.ImageUrl = productDto.ImageUrl;
        product.CategoryId = productDto.Category.Id;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return Ok(new ApiResponse<ProductDto>(productDto, true, "Product updated successfully"));
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(new ApiResponse<object>(null, false, "Product not found"));
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok(new ApiResponse<object>(null, true, "Product deleted successfully"));
    }
}
