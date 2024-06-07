namespace CarRent.Application.Features.Cars.Common;

public sealed record CarsResponse(IList<CarListItem>? Cars);