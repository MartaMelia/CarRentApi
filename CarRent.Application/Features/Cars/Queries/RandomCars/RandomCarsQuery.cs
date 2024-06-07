namespace CarRent.Application.Features.Cars.Queries.RandomCars;

public sealed record RandomCarsQuery() : IRequest<ErrorOr<CarsResponse>>;