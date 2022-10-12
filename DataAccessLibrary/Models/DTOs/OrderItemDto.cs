namespace DataAccessLibrary.Models.DTOs;

public class OrderItemsDto
{
    [Key]
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    //[JsonIgnore]
    public Guid ProductId { get; set; }
    public ProductDto? Product { get; set; }

    public OrderItemsDto()
    {

    }
}
