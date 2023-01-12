using SantaShop.Domain.Dto;

namespace SantaShop.Domain.Services;

public interface IGiftListService
{
    Task<Response<List<GiftList>>> GetGiftListAsync();
}