using Microsoft.EntityFrameworkCore;
using PhoneShop.API;
using PhoneShop.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for the frontend URL (localhost:5087)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                origin.StartsWith("http://localhost:5087") ||
                origin.StartsWith("https://localhost:5087"))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Add DbContext service to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Seed data if needed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<SeedData>>();

    // Run the seeding process
    SeedData.Initialize(services, logger);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy for all incoming requests
app.UseCors("AllowBlazorApp");

app.UseHttpsRedirection();

// Map the controllers for API endpoints
app.MapControllers();

app.Run();