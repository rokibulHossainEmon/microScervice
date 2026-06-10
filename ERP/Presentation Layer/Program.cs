using Domain_Layer.Interfaces;
using Persistence_Layer.Data;
using Persistence_Layer.Repositories;
using Application_Layer.Interfaces;
using Application_Layer.Scervices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers();

// 2. Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Register Database Context (Update ConnectionString in appsettings.json later)
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Register Unit of Work and Generic Repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 5. Register Services
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderService, OrderService>(); 
// builder.Services.AddScoped<IOrderService, OrderService>(); // Uncomment when created

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwagger();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();