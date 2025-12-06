using KorpInventory.Application.Commands.CreateProduct;
using KorpInventory.Core.Repository;
using KorpInventory.Infrastructure.Persistence;
using KorpInventory.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddMediatR(typeof(CreateProductCommand));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
