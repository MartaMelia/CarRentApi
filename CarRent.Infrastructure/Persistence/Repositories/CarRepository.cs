namespace CarRent.Infrastructure.Persistence.Repositories;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    public CarRepository(CarRentContext dbContext) : base(dbContext) {}
}
