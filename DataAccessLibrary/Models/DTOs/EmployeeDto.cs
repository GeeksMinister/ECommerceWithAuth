namespace DataAccessLibrary.Models.DTOs;

public class EmployeeDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime Birthdate { get; set; }

    [StringLength(20)]
    public string? City { get; set; }

    [Required]
    public string Role { get; set; } = "Employee";

    [StringLength(50)]
    [Range(0, Int64.MaxValue, ErrorMessage = "Contact number should not contain characters")]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(300)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A password is required")]
    public string Password { get; set; } = string.Empty;

    public EmployeeDto()
    {
        Birthdate = DateTime.Now.AddYears(-20);
    }
}
