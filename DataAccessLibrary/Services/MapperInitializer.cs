namespace DataAccessLibrary.Services;

public class MapperInitializer : Profile
{
	public MapperInitializer()
	{
		CreateMap<Employee, EmployeeDto>().ReverseMap();
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<Order, OrderDto>().ReverseMap();
		CreateMap<OrderItems, OrderItemsDto>().ReverseMap();
        CreateMap<string, string>().ConvertUsing(str => (str ?? "").Trim());

		CreateMap<CartItem, OrderItemsDto>().ReverseMap();
    }   
}
