namespace DataAccessLibrary.Data;

public class ECommerceDbContext : DbContext
{
	private readonly IConfiguration _config;

	public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options, IConfiguration config) : base(options)
	{
		_config = config;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		if (options.IsConfigured == false)
		{
			options.UseSqlServer(_config.GetConnectionString("Default"));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Order>()
			.HasMany(order => order.OrderItems)
			.WithOne(item => item.Order)
			.HasForeignKey(item => item.OrderId);

		modelBuilder.Entity<Category>()
			.HasMany(category => category.Products)
			.WithOne(product => product.Category)
			.HasForeignKey(product => product.CategoryId);
	}

	public DbSet<Order> Order { get; set; } = null!;
	public DbSet<OrderItems> OrderItems { get; set; } = null!;
    public DbSet<Employee> Employee { get; set; } = null!;
    public DbSet<Product> Product { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;

}
