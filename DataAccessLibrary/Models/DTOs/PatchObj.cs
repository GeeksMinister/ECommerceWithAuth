namespace DataAccessLibrary.Models.DTOs;

public class PatchObj
{
    public string Path { get; set; } = string.Empty; 
    public string Op { get; set; } = "replace";
    public string Value { get; set; } = string.Empty;
}
