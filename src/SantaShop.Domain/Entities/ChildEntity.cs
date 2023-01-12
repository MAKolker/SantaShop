namespace SantaShop.Domain;

public class ChildEntity:BaseEntity
{
    public string Name { get; set; }   
    public int Age { get; set; } 
    public string Address { get; set; } 
    
    public List<GiftRequestEntity> Requests { get; set; }

}