namespace CarRent.Infrastructure.Persistence;

public class CarRentContext : DbContext
{
    public CarRentContext(DbContextOptions<CarRentContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Car> Cars {  get; set; } = null!;
    public virtual DbSet<CarImage> CarsImages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CarRentContext).Assembly);
        base.OnModelCreating(builder);
    }
}
