
namespace CarRent.Application.Features.Cars.Queries.GetCars;

public class GetCarsHandler : IRequestHandler<GetCarsQuery, ErrorOr<CarsResponse>>
{
    private readonly ICarRepository _carRepository;

    public GetCarsHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ErrorOr<CarsResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        var entities = _carRepository
            .GetQueryable(x => !x.IsDeleted, x => x.Images, x => x.Brand, x => x.Model, x => x.City)
            .OrderBy(x => Guid.NewGuid());

        var cars = entities.Select(x => new CarListItem(
            x.Id,
            x.Brand.Value,
            x.Model.Value,
            x.City.Value,
            nameof(x.Transmission),
            x.DailyPrice.ToString(),
            x.ReleaseYear.ToString(),
            x.Sits.ToString(),
            $"data:image/png;base64,{Convert.ToBase64String(x.Images.First().Picture)}")).ToList();

        return new CarsResponse(cars);
    }
}
