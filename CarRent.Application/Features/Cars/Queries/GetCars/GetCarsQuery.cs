namespace CarRent.Application.Features.Cars.Queries.GetCars;

public sealed record GetCarsQuery() : IRequest<ErrorOr<CarsResponse>>;