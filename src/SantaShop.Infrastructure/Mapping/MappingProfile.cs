using AutoMapper;
using SantaShop.Domain;
using SantaShop.Domain.Dto;

namespace SantaShop.Infrastructure.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<GiftRequest, ChildEntity>().ForMember(c => c.Requests, opt => opt.Ignore());

    }
}