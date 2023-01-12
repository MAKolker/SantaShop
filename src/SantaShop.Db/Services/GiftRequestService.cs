using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SantaShop.Domain;
using SantaShop.Domain.Dto;
using SantaShop.Domain.Services;

namespace SantaShop.Db.Services;

public class GiftRequestService:IGiftRequestService
{

    private readonly ILogger<GiftRequestService> _logger;
    private readonly IDbContextFactory<GiftShopContext> _factory;
    private readonly IMapper _mapper;

    public GiftRequestService(ILogger<GiftRequestService> logger, IDbContextFactory<GiftShopContext> factory, IMapper mapper)
    {
        _logger = logger;
        _factory = factory;
        _mapper = mapper;
    }

    public async Task<Response<GiftRequest>> CreateGiftRequestAsync(GiftRequest request, CancellationToken token)
    {
        try
        {
            using var context = _factory.CreateDbContext();
            var child = _mapper.Map<ChildEntity>(request);
            child.Id = Guid.NewGuid();
            var requests = new List<GiftRequestEntity>();
            if (request.GiftsWanted.Any())
            {
                var giftNames = request.GiftsWanted.Select(g => g.Name).ToList();
                var gifts = await context.Gifts.Where(g => giftNames.Contains(g.Name)).ToListAsync(token);
                if (!gifts.Any() || (gifts.Count<giftNames.Count))
                    throw new BadHttpRequestException("Invalid gifts");
                var sum = gifts.Sum(g => g.Price);
                if (sum>50)
                    throw new BadHttpRequestException("Gift price exceeded");

                requests = gifts.Join(request.GiftsWanted, c => c.Name, g => g.Name, (c, g) => new GiftRequestEntity()
                {
                    ChildId = child.Id,
                    Color = g.Color,
                    GiftId = c.Id
                }).ToList();
            }

          
            await context.Children.AddAsync(child, token);

            if (requests.Any())
                await context.AddRangeAsync(requests, token);

            await context.SaveChangesAsync(token);

            return request with { Id = child.Id };


        }
        catch (Exception e)
        {
          _logger.LogError("Error creating GiftRequest", e);
          return e;
        }
        
    }

    public async Task<Response<GiftRequest>> CreateOrUpdateGiftRequestAsync(GiftRequest request, CancellationToken token)
    {
        try
        {
            
            using var context = _factory.CreateDbContext();

            var child = await context.Children.Include(c=>c.Requests).ThenInclude(g=>g.Gift
            ).FirstOrDefaultAsync(c =>
                c.Name.Equals(request.Name) && c.Age.Equals(request.Age));  //_mapper.Map<ChildEntity>(request);

            if (child == null)
                return await CreateGiftRequestAsync(request, token);
            child.Address = request.Address;
            context.Children.Update(child);

            var requests = new List<GiftRequestEntity>();
            if (request.GiftsWanted.Any())
            {
                var giftNames = child.Requests.ToDictionary(c => c.Gift.Name, c => c);
                var gifts = await context.Gifts.Where(g => request.GiftsWanted.Select(c=>c.Name).Contains(g.Name)).
                    ToDictionaryAsync(c=>c.Name, c=>c, token);
                var sum = child.Requests.Sum(g => g.Gift.Price);
                foreach (var info in request.GiftsWanted)
                {
                    if (giftNames.TryGetValue(info.Name, out var ent))
                    {
                        if (ent.Color != info.Color)
                        {
                            ent.Color = info.Color;
                            context.Update(ent);
                        }
                    }
                    else
                    {
                        if (gifts.TryGetValue(info.Name, out var gift))
                        {
                            if ((sum+gift.Price)>50)
                                throw new BadHttpRequestException("Gift price exceeded");
                            await context.GiftRequests.AddAsync(new GiftRequestEntity()
                                { ChildId = child.Id, GiftId = gift.Id, Color = info.Color });
                        }
                    }
                }
            }



            await context.SaveChangesAsync(token);

            return request with { Id = child.Id };


        }
        catch (Exception e)
        {
            _logger.LogError("Error creating GiftRequest", e);
            return e;
        }
    }
}