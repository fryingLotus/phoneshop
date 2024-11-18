// File: PhoneShop.Web/Services/CategoryService.cs

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PhoneShop.Shared;

namespace PhoneShop.Web.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to get all categories from the API
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<CategoryDto>>($"{ApiConstants.BaseUrl}/categories");
            return categories;
        }

        // Method to get a single category by ID
        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _httpClient.GetFromJsonAsync<CategoryDto>($"{ApiConstants.BaseUrl}/categories/{categoryId}");
            return category;
        }

        // Method to add a new category
        public async Task AddCategoryAsync(CategoryDto newCategory)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiConstants.BaseUrl}/categories", newCategory);
            response.EnsureSuccessStatusCode();
        }

        // Method to update an existing category
        public async Task UpdateCategoryAsync(CategoryDto updatedCategory)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiConstants.BaseUrl}/categories/{updatedCategory.Id}", updatedCategory);
            response.EnsureSuccessStatusCode();
        }

        // Method to delete a category
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var response = await _httpClient.DeleteAsync($"{ApiConstants.BaseUrl}/categories/{categoryId}");
            response.EnsureSuccessStatusCode();
        }
    }
}