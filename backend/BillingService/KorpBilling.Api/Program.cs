using KorpBilling.Application.Commands.CreateInvoice;
using KorpBilling.Core.Repository;
using KorpBilling.Infrastructure.Persistence;
using KorpBilling.Infrastructure.Persistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<BillingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddHttpClient();

builder.Services.AddMediatR(typeof(CreateInvoiceCommand));

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

// Configure the HTTP request pipeline.
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
