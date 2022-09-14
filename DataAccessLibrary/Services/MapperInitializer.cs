using DataAccessLibrary.DTOs;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Services;

public class MapperInitializer : Profile
{
	public MapperInitializer()
	{
		CreateMap<Employee, EmployeeDto>().ReverseMap();
	}   
}
