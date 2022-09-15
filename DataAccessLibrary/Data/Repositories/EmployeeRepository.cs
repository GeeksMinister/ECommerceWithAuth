using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly ECommerceDbContext _dbContext;

	public EmployeeRepository(ECommerceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Employee?> GetEmployeeById(Guid employeeId)
	{
		var result = await _dbContext.Employee.FirstOrDefaultAsync(emp => emp.Guid == employeeId);
		return result;
	}

	public async Task<Employee?> GetEmployeeByIdOrEmail(string loginInfo)
	{
		var result = await _dbContext.Employee.FirstOrDefaultAsync(
			emp => emp.Guid.ToString() == loginInfo || emp.Email == loginInfo);
		return result;
	}


}
