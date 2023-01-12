namespace SantaShop.Domain;

public class Response<T> where T:class
{
    
    public bool HasErrors { get; set; }
    
    public string ErrorMessage { get; set; }
    
    public T Object { get; set; }
    
    public static implicit operator Response<T>(T obj) => new Response<T>(){Object = obj, ErrorMessage = string.Empty, HasErrors = false};
    public static implicit operator Response<T>(Exception ex) => new Response<T>(){Object = default(T), ErrorMessage = ex.Message, HasErrors = true};
    
}