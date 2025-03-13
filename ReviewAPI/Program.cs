using Microsoft.EntityFrameworkCore;
using ReviewAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ReviewDbContext>(options =>
    options.UseSqlite(connectionString)); // Use SQLite instead of SQL Server

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Serve static files from wwwroot
app.UseRouting();     // Enables routing
app.UseAuthorization();

app.MapControllers(); // Maps API controllers

app.UseDefaultFiles(); // Serves index.html by default from wwwroot

app.Run(); // Start the application
