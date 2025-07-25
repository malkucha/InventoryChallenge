using AutoMapper;
using Business.BusinessLogic;
using Business.BusinessMapper;
using DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using InventoryService.Error;
using InventoryService.Mappers;
using InventoryService.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAplicationCoreDataAccess(connectionString!);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new ProductMapperProfile());
    cfg.AddProfile(new ViewModelMapperProfile());
});

builder.Services.AddSingleton<Publisher>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();

