namespace CarRent.Application.Features.Cars.Queries.GetCar;

public class GetCarHandler : IRequestHandler<GetCarQuery, ErrorOr<CarResponse>>
{
    private readonly ICarRepository _carRepository;

    public GetCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<ErrorOr<CarResponse>> Handle(GetCarQuery request, CancellationToken cancellationToken)
    {
        var entity = await _carRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id,
            x => x.Brand,
            x => x.Model,
            x => x.City,
            x => x.Images,
            x => x.User);

        if (entity is null) return Error.NotFound();

        CarResponse result = new(
            entity.Id,
            entity.Brand.Value,
            entity.Model.Value,
            entity.ReleaseYear.ToString(),
            nameof(entity.Transmission),
            entity.FuelCapacity.ToString(),
            entity.Sits.ToString(),
            entity.User.PhoneNumber,
            entity.City.Value,
            entity.DailyPrice.ToString(),
            entity.Images.Select(x => $"data:image/png;base64,{Convert.ToBase64String(x.Picture)}").ToList());

        return result;
    }
}
