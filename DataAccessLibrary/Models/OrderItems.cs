namespace DataAccessLibrary.Models;

public class OrderItems
{
    [Key]
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    [JsonIgnore]    
    public Order Order { get; set; } = new();
    public decimal? Price { get => Product?.Price * Quantity; }
    public int Quantity { get; set; }
    [JsonIgnore]
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public OrderItems()
    {

    }
}
