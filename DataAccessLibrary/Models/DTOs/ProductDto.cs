namespace DataAccessLibrary.Models.DTOs;

public class ProductDto
{
    public string Id { get; set; } = string.Empty;

    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name is too short. Check input!")]
    public string Name { get; set; } = string.Empty;

    [StringLength(512, ErrorMessage = "Too long Image-URL")]
    public string ImageURL { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [DataType(DataType.Date)]
    public DateTime DiscountUntil { get; set; } = DateTime.Now;

    [Range(0, 99, ErrorMessage = "Invalid Input!")]
    public decimal DiscountRate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Invalid value!")]
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public ProductDto()
    {

    }
}
