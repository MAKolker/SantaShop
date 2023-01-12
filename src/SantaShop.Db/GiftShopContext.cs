using Microsoft.EntityFrameworkCore;
using SantaShop.Db.Seed;
using SantaShop.Domain;

namespace SantaShop.Db;

public class GiftShopContext:DbContext
{
    public GiftShopContext(DbContextOptions<GiftShopContext> options)
        : base(options)
    {
        //this.Configuration.LazyLoadingEnabled = false;
    }

    public virtual DbSet<ChildEntity> Children { get; set; }
    public virtual DbSet<GiftsEntity> Gifts { get; set; }
    public virtual DbSet<GiftRequestEntity> GiftRequests { get; set; }

        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            
        #region Seed
        modelBuilder.AddGiftsSeed();
        #endregion
    }
}