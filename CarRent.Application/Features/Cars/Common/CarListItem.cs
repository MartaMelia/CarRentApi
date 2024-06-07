namespace CarRent.Application.Features.Cars.Common;

public sealed record CarListItem(
    Guid Id,
    string Brand,
    string Model,
    string City,
    string Transmition,
    string Price,
    string Year,
    string Passengers,
    string Picture);