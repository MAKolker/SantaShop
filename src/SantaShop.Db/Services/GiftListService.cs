using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SantaShop.Domain;
using SantaShop.Domain.Dto;
using SantaShop.Domain.Services;

namespace SantaShop.Db.Services;

public class GiftListService:IGiftListService
{
    private readonly ILogger<GiftListService> _logger;
    private readonly IDbContextFactory<GiftShopContext> _factory;


    public GiftListService(ILogger<GiftListService> logger, IDbContextFactory<GiftShopContext> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task<Response<List<GiftList>>> GetGiftListAsync()
    {
        using var context = _factory.CreateDbContext();
        var result = await context.Children
            .Include(c => c.Requests)
            .ThenInclude(g => g.Gift)
            .GroupBy(c => c.Address, v => v.Requests.Select(v => v.Gift.Name),
                (k, v) => new GiftList(k, v.SelectMany(v => v.ToArray()).Distinct().ToArray())).ToListAsync();

        return result;

    }
}