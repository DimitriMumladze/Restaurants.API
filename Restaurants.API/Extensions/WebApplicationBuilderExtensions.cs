using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;
using Microsoft.AspNetCore.Routing;
using Restaurants.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Restaurants.Infrastructure.Seeders;
using System.Threading.Tasks;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        // Add base services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Add middleware services
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        // Configure Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    []
                }
            });
        });

        // Configure Serilog
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );

        return builder;
    }

    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        // Configure middleware
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeLoggingMiddleware>();

        // Configure Serilog request logging
        app.UseSerilogRequestLogging();

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();
        }

        // Configure Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurants API v1"));
        }

        // Configure middleware pipeline
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Configure Identity
        app.MapGroup("api/identity").MapIdentityApi<User>();

        return app;
    }
}