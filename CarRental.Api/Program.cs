using CarRental.API.Middleware;
using CarRental.Application;
using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using CarRental.Infrastructure.DAL;
using CarRental.Infrastructure.Repositories;
using CarRental.Infrastructure.Services;
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
builder.Services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<CarSeeder>();
builder.Services.AddTransient<ICarRentalService, CarRentalService>();
builder.Services.AddScoped<ICarRentalRepository, CarRentalRepository>();
builder.Services.AddScoped<CarErrorHandlingMiddleware>();
//autoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

EmailSettings.ApiKey = builder.Configuration.GetValue<string>("MailSettings:Key");
EmailSettings.Message = builder.Configuration.GetValue<string>("MailSettings:Message");
EmailSettings.Email = builder.Configuration.GetValue<string>("MailSettings:Email");
CarDatabaseSettings.DbConnectionString = builder.Configuration.GetValue<string>("DatabaseSettings:DbConnectionString");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CarSeeder>();
    seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();