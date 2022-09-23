namespace DataAccessLibrary.Services.RefitClient;

public interface IEmployeeClientData
{
    [Post("/Employee/Login")]
    Task<AuthenticatedUserModel> Login(object obj);

    [Post("/Employee/RequestToken")]
    Task<string> RequestToken(string loginInfo, string employeeId);
    
}
