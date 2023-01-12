using SantaShop.Domain.Dto;

namespace SantaShop.Domain.Services;

public interface IGiftRequestService
{
    Task<Response<GiftRequest>> CreateGiftRequestAsync(GiftRequest request, CancellationToken token);
    
    Task<Response<GiftRequest>> CreateOrUpdateGiftRequestAsync(GiftRequest request, CancellationToken token);
}