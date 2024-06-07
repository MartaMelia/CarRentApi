namespace CarRent.Domain.Entities.Cars;

public class CarImage : Aggregate
{
    public Guid CarId { get; private set; }
    public byte[] Picture { get; private set; } = null!;

    public virtual Car Car { get; private set; } = null!;

    public CarImage() {}

    public CarImage(Guid id, Guid carId, byte[] picture) : base(id, DateTimeOffset.Now) 
    {
        CarId = carId;
        Picture = picture;
    }
}
