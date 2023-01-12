using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaShop.Domain;

namespace SantaShop.Db.Configuration;

public class GiftRequestConfiguration:IEntityTypeConfiguration<GiftRequestEntity>
{
    public void Configure(EntityTypeBuilder<GiftRequestEntity> builder)
    {
        builder.ToTable("GiftRequest");
        builder.HasKey(c => new { c.ChildId, c.GiftId });
       
        // relationships
        builder.HasOne(t => t.Child)
            .WithMany(t => t.Requests)
            .HasForeignKey(d => d.ChildId)
            .HasConstraintName("FK_GiftRequest_Child");
       
        builder.HasOne(t => t.Gift)
            .WithMany(t => t.Requests)
            .HasForeignKey(d => d.GiftId)
            .HasConstraintName("FK_GiftRequest_Gift");
    }
}