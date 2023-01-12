using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaShop.Domain;

namespace SantaShop.Db.Configuration;

public class ChildEntityConfiguration: IEntityTypeConfiguration<ChildEntity>
{
    public void Configure(EntityTypeBuilder<ChildEntity> builder)
    {
        builder.ToTable("Children");
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.Name, c.Age }).IsUnique();

        builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Age).IsRequired();
        builder.Property(c => c.Address).IsRequired().HasMaxLength(1024);
        
    }
}