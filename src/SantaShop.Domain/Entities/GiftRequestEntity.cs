namespace SantaShop.Domain;

public class GiftRequestEntity
{
    public Guid ChildId { get; set; }
    public Guid GiftId { get; set; }
    public string Color { get; set; }
    
    public virtual ChildEntity Child { get; set; }
    
    public virtual GiftsEntity Gift { get; set; }
}