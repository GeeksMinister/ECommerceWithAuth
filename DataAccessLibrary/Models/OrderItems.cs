namespace DataAccessLibrary.Models;

public class OrderItems
{
    [Key]
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Price { get => Product.Price * Quantity; }
}
