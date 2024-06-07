namespace CarRent.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(CarRentContext dbContext) : base(dbContext) { }
}