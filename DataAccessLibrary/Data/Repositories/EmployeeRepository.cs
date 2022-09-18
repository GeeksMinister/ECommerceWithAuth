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
		return await _dbContext.Employee.FirstOrDefaultAsync(emp => emp.Guid.Equals(employeeId));
	}

	public async Task<Employee?> GetEmployeeByIdOrEmail(string loginInfo)
	{
		var result = await _dbContext.Employee.FirstOrDefaultAsync(
			emp => emp.Guid.ToString().Equals(loginInfo) || emp.Email.Equals(loginInfo));

		return result;
	}


}
