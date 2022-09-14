namespace DataAccessLibrary.Models.DTOs;

public class AuthenticatedUserModel
{
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Access_Token { get; set; }

}
