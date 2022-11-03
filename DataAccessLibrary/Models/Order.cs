namespace DataAccessLibrary.Models;

public class Order
{
    public Guid OrderId { get; set; }

    [Required]
    [DisplayName("First Name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name should be between 3 - 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Last Name")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name should be between 3 - 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [DisplayName("City")]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [DisplayName("Postal Code")]
    [StringLength(10, ErrorMessage = "Postal code is too long. Check your input")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Enter a valid phone number")]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    [DataType(DataType.Date)]
    public string OrderPlaced { get; set; } = DateTime.Now.ToString("yyy-MM-dd");

    public decimal? TotalToPay { get; set; }

    public List<OrderItems> OrderItems { get; set; } = new();

    public string Customername() => FirstName + ' ' + LastName;


    public Order()
    {

    }
}
