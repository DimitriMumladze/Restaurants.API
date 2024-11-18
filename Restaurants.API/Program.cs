using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Restaurants.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurants API", Version = "v1" });
});

// Add this line here, before building the app
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

//Seeder
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();
//End of the seeder

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurants API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();