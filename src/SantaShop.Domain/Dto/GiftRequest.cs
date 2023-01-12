namespace SantaShop.Domain.Dto;

public sealed record GiftRequest(Guid? Id, string Name, int Age, string Address, List<GiftInfo> GiftsWanted);