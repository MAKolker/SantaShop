using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SantaShop.Common.Options;

namespace SantaShop.Db;

public static class DBRegistrationModule
{
    public static IServiceCollection RegisterGiftShopContext(this IServiceCollection services, IOptions<DatabaseOptions> dbOptions)
    {
        ILoggerFactory consoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        var connectionString = dbOptions.Value.BuildConnectionString();
            
        services.AddPooledDbContextFactory<GiftShopContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseLoggerFactory(consoleLoggerFactory);
          options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
            
        return services;
    }
}