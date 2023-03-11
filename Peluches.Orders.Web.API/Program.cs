using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Peluches.Orders.Web.API.Application.Dtos;
using Peluches.Orders.Web.API.Application.Services;
using Peluches.Orders.Web.API.Models;
using Peluches.Orders.Web.API.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TiendaPeluchesDBAzureContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("TiendaPeluchesDBAzureContext")));

builder.Services.AddScoped<IValidator<OrderUpdateDto>, OrderUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<OrderDetailsUpdateDto>, OrderDetailUpdateDtoValidator>();

builder.Services.AddScoped<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
builder.Services.AddScoped<IValidator<OrderDetailsCreateDto>, OrderDetailCreateDtoValidator>();

builder.Services.AddScoped<IOrderService, OrderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
