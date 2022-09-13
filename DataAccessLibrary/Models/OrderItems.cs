namespace DataAccessLibrary.Models;
public class OrderItems
{
    [Key]
    public Guid Guid { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
