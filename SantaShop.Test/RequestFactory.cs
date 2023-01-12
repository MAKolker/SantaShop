using AutoFixture;
using SantaShop.Domain.Dto;

namespace SantaShop.Test;

public class RequestFactory
{
    private Fixture _fixture;
    private Random _rnd;

    public RequestFactory()
    {
        _fixture = new Fixture();
        _rnd = new Random();
    }

    public GiftRequest CreateEmptyGiftRequest() =>
        _fixture.Build<GiftRequest>()
            .With(c=>c.Age, _rnd.Next(1,50))
            .With(c => c.GiftsWanted, new List<GiftInfo>()).Create();
}