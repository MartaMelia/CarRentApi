namespace CarRent.Domain.Entities.Cars;

public class Car : Aggregate
{
    public Guid BrandId { get; private set; }
    public Guid ModelId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CityId { get; private set; }
    public int ReleaseYear { get; private set; }
    public int Sits { get; private set; }
    public int FuelCapacity { get; private set; }
    public decimal DailyPrice { get; private set; }
    public TransmissionType Transmission { get; private set; }

    public virtual Category Brand { get; private set; } = null!;
    public virtual Category Model { get; private set; } = null!;
    public virtual Category City { get; private set; } = null!;
    public virtual User User { get; private set; } = null!;

    private List<CarImage> _images = new();
    public IReadOnlyCollection<CarImage> Images => _images;

    public Car() { }

    public Car(
        Guid id,
        Guid brandId,
        Guid modelId,
        Guid userId,
        Guid cityId,
        int releaseYear,
        int sits,
        int fuelCapacity,
        decimal dailyPrice,
        TransmissionType transmission) : base(id, DateTimeOffset.Now) 
    {
        BrandId = brandId;
        ModelId = modelId;
        UserId = userId;
        CityId = cityId;
        ReleaseYear = releaseYear;
        Sits = sits;
        FuelCapacity = fuelCapacity;
        DailyPrice = dailyPrice;
        Transmission = transmission;

        Validate(this);
    }

    private void Validate(Car car) 
    {
        if (car.BrandId == default) throw new Exception("უნიკალური იდენთიფიკატორი არავალიდურია");
        if (car.ModelId == default) throw new Exception("უნიკალური იდენთიფიკატორი არავალიდურია");
        if (car.UserId == default) throw new Exception("უნიკალური იდენთიფიკატორი არავალიდურია");
        if (car.CityId == default) throw new Exception("უნიკალური იდენთიფიკატორი არავალიდურია");

        if (car.ReleaseYear < 0) throw new Exception("გამოშვების წელი არავალიდურია");
        if (car.Sits < 0) throw new Exception("დასაჯდომების რაოდენობა არავალიდურია");
        if (car.FuelCapacity < 0) throw new Exception("ავზის მოცულობა არავალიდურია");
        if (car.DailyPrice < 0) throw new Exception("დღიური ღირებულება არავალიდურია");
    }
}
