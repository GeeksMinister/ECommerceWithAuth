namespace ECommerceWithAuth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IConfiguration _config;

    public EmployeeController(IEmployeeRepository employeeRepository, IConfiguration config)
    {
        _employeeRepository = employeeRepository;
        _config = config;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(AuthenticationUserModel authentication)
    {
        var employee = await _employeeRepository.GetEmployeeByIdOrEmail(authentication.LoginInfo);
        if (employee == null) return NotFound("Login Info Wasn't Found");

        (var passwordHash, var passwordSalt) = (employee.PasswordHash!, employee.PasswordSalt!);
        if (VerifyPassword(authentication.Password, passwordHash, passwordSalt) == false)
        {
            return BadRequest("Wrong Password");
        }
        var authenticatedUser = new AuthenticatedUserModel
        {
            Username = employee.FirstName + ' ' + employee.LastName,
            Email = employee.Email,
            Access_Token = CreateToken(employee)
        };

        return Ok(authenticatedUser);
    }

    private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordSalt = hmac.Key;
        return (passwordHash, passwordSalt);
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(Employee employee)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, employee.Username()),
            new Claim(ClaimTypes.Role, employee.Role),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.SerialNumber, employee.Guid.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config["JwtSettings:SigningKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds,
            expires: DateTime.Now.AddDays(1));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
