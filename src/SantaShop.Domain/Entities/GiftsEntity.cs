namespace SantaShop.Domain;

public class GiftsEntity:BaseEntity
{
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public List<GiftRequestEntity> Requests { get; set; }
}