namespace CarRent.Infrastructure.Persistence.Repositories;

public class CarImageRepository : BaseRepository<CarImage>, ICarImageRepository
{
    public CarImageRepository(CarRentContext dbContext) : base(dbContext) {}
}
