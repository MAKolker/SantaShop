using Microsoft.EntityFrameworkCore;
using SantaShop.Domain;

namespace SantaShop.Db.Seed;

public static class GiftSeedExtension
{
    public static void AddGiftsSeed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GiftsEntity>().HasData(
                new[]
                {
                    new GiftsEntity()
                    {
                        Id = new Guid("aaa4862e-edeb-4a38-93d8-18d1027fe030"),
                        Name = "PSP",
                        Price = 50
                    },
                    new GiftsEntity()
                    {
                        Id = new Guid("deb212df-a31a-4208-a8fa-cc3dc84f34b5"),
                        Name = "Rocket",
                       Price = 45
                    }, 
                    new GiftsEntity()
                    {
                        Id = new Guid("f3e2cc25-264c-4eff-ab71-b04f829ebbd6"),
                        Name = "RC Car",
                        Price = 25
                    }, 
                    new GiftsEntity()
                    {
                        Id = new Guid("e18f979c-5770-4696-b33e-8010d3c2849f"),
                        Name = "Lego",
                        Price = 15
                    }, 
                    new GiftsEntity()
                    {
                        Id = new Guid("101b175e-e102-477e-ab21-c975c001c3b9"),
                        
                        Name = "Barbie",
                        Price = 10
                    },
                    new GiftsEntity()
                    {
                        Id = new Guid("6f339fa9-da7e-4e2d-8000-337c6c7e8697"),
                        Name = "Cryon’s",
                       Price = 10
                    },
                    new GiftsEntity()
                    {
                        Id = new Guid("a4acd1b5-61e7-4dfe-bf0e-37c4659d41e6"),
                       
                        Name = "Candies",
                        Price = 5
                    },
                    new GiftsEntity()
                    {
                        Id = new Guid("1d92ef48-9a6e-4f1f-bf3d-bfc6d12513b6"),
                        Name = "Mittens",
                        Price = 5
                    }
                });
    }
}