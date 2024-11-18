// File: PhoneShop.API/Controllers/CategoryController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShop.API.Data;
using PhoneShop.API.Models;
using PhoneShop.Shared;

namespace PhoneShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<CategoryDto>>(null, false, "No categories found"));
            }

            return Ok(new ApiResponse<IEnumerable<CategoryDto>>(categories, true, "Categories fetched successfully"));
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound(new ApiResponse<CategoryDto>(null, false, "Category not found"));
            }

            return Ok(new ApiResponse<CategoryDto>(category, true, "Category fetched successfully"));
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            categoryDto.Id = category.Id; // Update the DTO with the new category ID
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, new ApiResponse<CategoryDto>(categoryDto, true, "Category created successfully"));
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new ApiResponse<CategoryDto>(null, false, "Category not found"));
            }

            category.Name = categoryDto.Name;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<CategoryDto>(categoryDto, true, "Category updated successfully"));
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new ApiResponse<object>(null, false, "Category not found"));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<object>(null, true, "Category deleted successfully"));
        }
    }
}
