namespace DataAccessLibrary.Models.DTOs;

public class ProductDto
{
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name is too short. Check input!")]
    public string Name { get; set; } = string.Empty;

    [StringLength(512, ErrorMessage = "Too long Image-URL")]
    public string ImageURL { get; set; } = string.Empty;

    public int Quantity { get; set; }

    [DataType(DataType.Date)]
    public DateTime DiscountUntil { get; set; }

    public decimal DiscountRate { get; set; }

    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public ProductDto()
    {

    }
}
