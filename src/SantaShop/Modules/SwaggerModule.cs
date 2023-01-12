using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SantaShop.Common.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SantaShop.Modules;

public static class SwaggerModule
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, ServiceProvider sp)
    {
        var serviceOptions = sp.GetService<IOptions<ServiceOptions>>();
        services.AddSwaggerGen((c) =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = serviceOptions!.Value!.Service_Title,
                Version = serviceOptions!.Value!.Service_Api_Version,
                Description = serviceOptions!.Value!.Service_Description,
                License = new OpenApiLicense
                {
                    Name = "",
                }
            });
            foreach (var filePath in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory)),
                         "*.xml"))
            {
                if (filePath.Contains("SantaShop"))
                {
                    c.IncludeXmlComments(filePath);
                }
            }
            c.MapType<Nullable<JsonElement>>(() => new OpenApiSchema { Type = "object" });
            c.MapType<JsonElement>(() => new OpenApiSchema { Type = "object" });
        });
        services.AddSwaggerExamples();


        return services;
    }

}