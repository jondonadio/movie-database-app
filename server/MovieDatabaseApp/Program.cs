using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieDatabaseApp.Data;
using MovieDatabaseApp.Interfaces;
using MovieDatabaseApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository with dependency injection
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieDatabaseApp", Version = "v1" });
});

// Add CORS services
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        builder => {
            // React frontend origin
            builder.WithOrigins("http://localhost:3000") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
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

// Enable CORS with the specified AllowReactApp policy
app.UseCors("AllowReactApp"); 

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
