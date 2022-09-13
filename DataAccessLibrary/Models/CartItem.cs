namespace DataAccessLibrary.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }

}
