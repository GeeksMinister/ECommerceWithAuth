namespace DataAccessLibrary.Models;

public class Category
{
    [Key]
    public Guid Guid { get; set; }

    [StringLength(150, ErrorMessage = "Name is too long")]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public List<Product> Products { get; set; } = new();
}
