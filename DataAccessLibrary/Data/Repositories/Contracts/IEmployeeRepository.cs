namespace DataAccessLibrary.Data.Repositories.Contracts;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeById(Guid employeeId);
    Task<Employee?> GetEmployeeByIdOrEmail(string loginInfo);
}