using System.Configuration;
//using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using IAM.Application;
using IAM.Infrastructure;
using IAM.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication().AddInfrastructure().AddPresentation();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options =>{
    options.AddPolicy("AllowOrigin",builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();