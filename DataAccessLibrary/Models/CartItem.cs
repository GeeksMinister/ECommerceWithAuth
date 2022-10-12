namespace DataAccessLibrary.Models;

public class CartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }
    public decimal Price { get; set; }
    
    public int GetValidQuantity()
    {
        if (Quantity > MaxQuantity)
        {
            Quantity = MaxQuantity;
        }
        if (Quantity <= 0)
        {
            Quantity = 1;
        }

        return Quantity;
    }

}

