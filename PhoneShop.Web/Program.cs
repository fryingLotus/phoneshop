using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhoneShop.Web;
using PhoneShop.Web.Services;  // Import your services

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient to be injected into services and components
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register HttpClientFactory for usage with AddHttpClient
builder.Services.AddHttpClient();

// Register ProductService as a scoped service
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

await builder.Build().RunAsync();