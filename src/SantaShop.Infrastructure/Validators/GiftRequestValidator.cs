using FluentValidation;
using SantaShop.Domain.Dto;

namespace SantaShop.Infrastructure.Validators;

public class GiftRequestValidator:AbstractValidator<GiftRequest>
{
    public GiftRequestValidator()
    {
        var hs = new HashSet<string>();
        
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Address).NotNull().NotEmpty();
        RuleFor(x => x.Age).NotNull().Must(c=>c>=1 && c<=100);
        When(x => x.GiftsWanted.Any(), () =>
        {
            RuleFor(x => x.GiftsWanted)
                .Must(c => c.All(v => hs.Add(v.Name))).WithMessage("Gifts must be unique");
        });
    }
}