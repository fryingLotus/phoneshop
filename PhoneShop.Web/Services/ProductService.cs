using System.Net.Http.Json;
using PhoneShop.Shared;

namespace PhoneShop.Web.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to get all products from the API
        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<ProductDto>>>($"{ApiConstants.BaseUrl}/products");
    
            return response?.Data ?? new List<ProductDto>();
        }

        // Method to get a single product by ID
        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            // Use the ApiConstants.BaseUrl to construct the request URL
            var product = await _httpClient.GetFromJsonAsync<ProductDto>($"{ApiConstants.BaseUrl}/products/{productId}");
            return product;
        }

        // Method to add a new product
        public async Task AddProductAsync(ProductDto newProduct)
        {
            // Use the ApiConstants.BaseUrl to construct the request URL
            var response = await _httpClient.PostAsJsonAsync($"{ApiConstants.BaseUrl}/products", newProduct);
            response.EnsureSuccessStatusCode();
        }

        // Method to update an existing product
        public async Task UpdateProductAsync(ProductDto updatedProduct)
        {
            // Use the ApiConstants.BaseUrl to construct the request URL
            var response = await _httpClient.PutAsJsonAsync($"{ApiConstants.BaseUrl}/products/{updatedProduct.Id}", updatedProduct);
            response.EnsureSuccessStatusCode();
        }

        // Method to delete a product
        public async Task DeleteProductAsync(int productId)
        {
            // Use the ApiConstants.BaseUrl to construct the request URL
            var response = await _httpClient.DeleteAsync($"{ApiConstants.BaseUrl}/products/{productId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
