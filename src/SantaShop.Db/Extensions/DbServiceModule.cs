using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SantaShop.Db.Services;
using SantaShop.Domain.Services;

namespace SantaShop.Db;

public static class DbServiceModule
{
    public static IServiceCollection AddDbServices(this IServiceCollection services)
    {
        services.AddScoped<IGiftRequestService, GiftRequestService>();
        services.AddScoped<IGiftListService, GiftListService>();
        return services;
    }
}