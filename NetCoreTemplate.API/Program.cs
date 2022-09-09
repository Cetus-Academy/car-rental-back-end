using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCoreTemplate.API.Extensions;
using NetCoreTemplate.API.Filters;
using NetCoreTemplate.Application;
using NetCoreTemplate.Application.Common.Services;
using NetCoreTemplate.Infrastructure;
using NetCoreTemplate.Infrastructure.Common.Services;
using NetCoreTemplate.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddControllers(opt => opt.Filters.Add(new ApiExceptionFilterAttribute()))
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.WebHost.UseSerilog();

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    //Custom response from ModelValidation
    opt.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(a => a.Key, a => a.Value?.Errors.Select(b => b.ErrorMessage).ToArray());

        return new BadRequestObjectResult(new
        {
            Title = "One or more validation failures have occurred",
            StatusCode = 400,
            Errors = errors
        });
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddHttpClient();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<ICurrentUserService,CurrentUserService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreTemplate.API v1"));

builder.Services.AddFluentValidation();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unable to run application");
}
finally
{
    Log.CloseAndFlush();
}

app.Run();