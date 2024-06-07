namespace CarRent.Application.Features.Cars.Queries.GetCar;

public sealed record GetCarQuery(Guid Id) : IRequest<ErrorOr<CarResponse>>;
