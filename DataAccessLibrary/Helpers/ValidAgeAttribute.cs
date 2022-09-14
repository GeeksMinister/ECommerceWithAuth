namespace DataAccessLibrary.Helpers;

public class ValidAgeAttribute : ValidationAttribute
{
    public ValidAgeAttribute() { }

    public string GetErrorMessage() => "Employee must be between 18 - 100 years old";

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        int years = Convert.ToDateTime(value).Year;
        int age = DateTime.Now.Year - years;

        if (age < 18 || age >= 100)
        {
            return new ValidationResult(GetErrorMessage());
        }
        else
        {
            return ValidationResult.Success!;
        }        
    }
}