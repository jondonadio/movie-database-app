using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using MovieDatabaseApp.Data;
using MovieDatabaseApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MovieContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        builder => {
            builder.WithOrigins("http://localhost:3000") // Add the correct origin for your React app
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieDatabaseApp", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieDatabaseApp v1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp"); // Enable CORS with the specified policy

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed the Database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Seeding the database...");
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding to the DB.");
    }
}

app.Run();
