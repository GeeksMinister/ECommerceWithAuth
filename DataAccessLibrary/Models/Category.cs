namespace DataAccessLibrary.Models;
public class Category
{
    [Key]
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
}
