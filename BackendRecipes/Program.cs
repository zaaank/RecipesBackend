﻿using BackendRecipes.API.Data;
using HotelListing.API.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
var connectionString = builder.Configuration.GetConnectionString("RecipeDbConnectionString");
builder.Services.AddDbContext<RecipesDbContext>(options =>
{
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCORSpolicy", b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});
builder.Services.AddAutoMapper(typeof(MapperConfig));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCORSpolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

