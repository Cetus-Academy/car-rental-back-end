
using CarRental.Application.Shared;
using CarRental.Shared.Services;
using CarRentalAPI.DAL;
using CarRentalAPI.Entities;
using CarRentalAPI.Migrations;
using CarRentalAPI.Services;
using EmailService;
using EmailService.Common;
using EmailService.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CarDbContext>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailSendingRepository, EmailSendingRepository>();
//builder.Services.AddEmailService();
builder.Services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<CarRentalAPI.DAL.CarSeeder>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

EmailSettings.ApiKey = builder.Configuration.GetValue<string>("MailSettings:Key");
EmailSettings.Message = builder.Configuration.GetValue<string>("MailSettings:Message");
EmailSettings.Email = builder.Configuration.GetValue<string>("MailSettings:Email");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CarRentalAPI.DAL.CarSeeder>();
    seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
