using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaShop.Domain;

namespace SantaShop.Db.Configuration;

public class GiftsEntityConfiguration:IEntityTypeConfiguration<GiftsEntity>
{
    public void Configure(EntityTypeBuilder<GiftsEntity> builder)
    {
        builder.ToTable("Gifts");
        builder.HasKey(c => c.Id);
    }
}