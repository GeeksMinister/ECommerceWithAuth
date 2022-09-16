using DataAccessLibrary.DTOs;
using DataAccessLibrary.Models;
using DataAccessLibrary.Models.DTOs;

namespace DataAccessLibrary.Services;

public class MapperInitializer : Profile
{
	public MapperInitializer()
	{
		CreateMap<Employee, EmployeeDto>().ReverseMap();
		CreateMap<Product, ProductDto>().ReverseMap();
	}   
}
