using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Peluches.Administration.Web.API.Models;
using Peluches.Administration.Web.API.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<EmployeeValidatorAttribute>(int.MinValue);
    options.Filters.Add<ProductValidatorAttribute>(int.MinValue);
});
//builder.Services.AddControllers();

builder.Services.AddDbContext<TiendaPeluchesDBAzureContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("TiendaPeluchesDBAzureContext")));

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
