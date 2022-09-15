namespace DataAccessLibrary.Data.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeById(Guid employeeId);
    Task<Employee?> GetEmployeeByIdOrEmail(string loginInfo);
}