namespace ECommerceWithAuth.MVC.Data;

public class ECommerceDbContext : ECommerceWithAuthMVCContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {

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
