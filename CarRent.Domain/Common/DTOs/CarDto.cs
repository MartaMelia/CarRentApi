namespace CarRent.Domain.Common.DTOs;

public class CarDto
{
    public string Model { get; set; } = null!;
    public List<string>? Pictures { get; set; }
}
