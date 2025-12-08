using FluentValidation;
using FluentValidation.AspNetCore;
using KorpInventory.Application.Commands.CreateProduct;
using KorpInventory.Application.Validators;
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
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNext", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseCors("AllowNext");
app.MapScalarApiReference();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
