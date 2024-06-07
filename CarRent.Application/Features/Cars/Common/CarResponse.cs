namespace CarRent.Application.Features.Cars.Common;

public sealed record CarResponse(
    Guid Id,
    string Brand,
    string Model,
    string Year,
    string Transmition,
    string FuelCapacity,
    string Passangers,
    string PhoneNumber,
    string City,
    string Price,
    List<string> Pictures);