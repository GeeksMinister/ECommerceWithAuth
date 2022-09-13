namespace DataAccessLibrary.Models;
public class Product
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool InStock { get => Quantity > 0; }
    public bool OnSale { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new();
}
