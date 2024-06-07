namespace CarRent.Infrastructure.Persistence.Configuration.Seeds;

public class Seed
{
    private readonly CarRentContext _context;

    public Seed(CarRentContext context)
    {
        _context = context;
    }

    public void Init()
    {
        if (_context.Categories.Any()) return;

        string userJsonFilePath = Path.Combine("Common", "Data", "User.json");
        string userJsonString = File.ReadAllText(userJsonFilePath);

        UserDto userDto = JsonSerializer.Deserialize<UserDto>(userJsonString)!;

        User user = new(
            Guid.NewGuid(),
            userDto.FirstName,
            userDto.LastName,
            userDto.PhoneNumber,
            userDto.Email,
            userDto.PasswordHash,
            userDto.PasswordSalt,
            userDto.Role,
            null,
            null);

        _context.Add(user);

        string categoryJsonFilePath = Path.Combine("Common", "Data", "Categories.json");
        string categoryJsonString = File.ReadAllText(categoryJsonFilePath);

        List<CategoryDto> categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryJsonString)!;

        List<Category> categoryEntities = new List<Category>();

        foreach (var category in categories)
        {
            var categoryEntity = new Category(
                Guid.NewGuid(),
                category.Value,
                null,
                category.Type);

            categoryEntities.Add(categoryEntity);

            if (category.Children != null) {
                foreach (var child in category.Children)
                {
                    var childEntity = new Category(
                        Guid.NewGuid(),
                        child.Value,
                        categoryEntity.Id,
                        child.Type);

                    categoryEntities.Add(childEntity);
                }
            }
        }

        _context.Categories.AddRange(categoryEntities);

        string carJsonFilePath = Path.Combine("Common", "Data", "Cars.json");
        string carJsonString = File.ReadAllText(carJsonFilePath);

        List<CarDto> carDtos = JsonSerializer.Deserialize<List<CarDto>>(carJsonString)!;
        List<Car> carEntities = new();
        List<CarImage> carImageEntities = new();

        var cities = categoryEntities.Where(x => x.Type == Domain.Common.Enums.Category.CategoryType.City).Select(x => x.Id).ToList();
        var cityCount = cities.Count();

        var models = categoryEntities.Where(x => x.Type == Domain.Common.Enums.Category.CategoryType.CarModel).ToDictionary(x => x.Value, x => x);

        foreach (var car in carDtos)
        {
            Random random = new Random();
            var model = models.GetValueOrDefault(car.Model);

            if (model is null) continue;

            Car carEntity = new(
                Guid.NewGuid(),
                model.ParentId!.Value,
                model.Id,
                user.Id,
                cities[random.Next(cityCount)],
                random.Next(2015, 2024),
                random.Next(2, 4),
                random.Next(15, 100),
                GenerateRandomDecimal(random, 50, 600),
                Transmission(random.Next(3)));

            carEntities.Add(carEntity);

            if (car.Pictures != null) 
            {
                foreach (var link in car.Pictures) 
                {
                    if (string.IsNullOrEmpty(link)) continue;

                    using (HttpClient client = new HttpClient())
                    {
                        byte[] picture = client.GetByteArrayAsync(link).Result;

                        CarImage carImageEntity = new(Guid.NewGuid(), carEntity.Id, picture);

                        carImageEntities.Add(carImageEntity);
                    }   
                }
            }
        }

        _context.Cars.AddRange(carEntities);
        _context.CarsImages.AddRange(carImageEntities);

        _context.SaveChanges();
    }

    private static TransmissionType Transmission(int number) => number switch
    {
        0 => TransmissionType.Manual,
        1 => TransmissionType.Automatic,
        2 => TransmissionType.Tiptronic,
        _ => TransmissionType.Manual
    };

    private static decimal GenerateRandomDecimal(Random random, decimal minValue, decimal maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentException("minValue should be less than or equal to maxValue");
        }

        double randomDouble = random.NextDouble();

        decimal scaledDecimal = (decimal)randomDouble * (maxValue - minValue) + minValue;

        return scaledDecimal;
    }
}
