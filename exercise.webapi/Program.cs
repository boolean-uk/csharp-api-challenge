using exercise.webapi.Repositories;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using exercise.webapi.Data;
using exercise.webapi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IRepository, DishwasherRepository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Dishwasher"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.ConfigureDishwasherEndpoints();
app.MapControllers();

app.Run();
