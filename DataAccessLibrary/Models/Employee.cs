namespace DataAccessLibrary.Models;

public class Employee
{
    [Key]
    [StringLength(36)]
    public Guid Guid { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [ValidAge]
    [DataType(DataType.Date)]
    //[StringLength(10)]
    public string Birthdate { get; set; } = string.Empty;

    [StringLength(50)]
    [Phone]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(300)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? City { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Max characters (50)")]
    public string Role { get; set; } = "Admin";

    [StringLength(150, ErrorMessage = "Too long Password-Hash detected")]
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    [StringLength(512)]
    public string? RefreshToken { get; set; }
    public DateTime? TokenCreated { get; set; }
    public DateTime? TokenExpires { get; set; }

    public Employee() { }

    public string Username() => FirstName + " " + LastName;
}
