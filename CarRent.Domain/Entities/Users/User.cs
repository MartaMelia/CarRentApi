namespace CarRent.Domain.Entities.Users;

public class User : Aggregate
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string PasswordSalt { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTimeOffset? RefreshTokenExpires { get; private set; }

    public User() {}

    public User(
        Guid id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string passwordHash,
        string passwordSalt,
        UserRole role,
        string? refreshToken,
        DateTimeOffset? refreshTokenExpires) : base(id, DateTimeOffset.Now)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Role = role;
        RefreshToken = refreshToken;
        RefreshTokenExpires = refreshTokenExpires;

        Validate(this);
    }

    public void Update(
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string passwordHash,
        string passwordSalt,
        UserRole role,
        string? refreshToken,
        DateTimeOffset? refreshTokenExpires) 
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Role = role;
        RefreshToken = refreshToken;
        RefreshTokenExpires = refreshTokenExpires;

        Validate(this);
        Update(DateTimeOffset.Now);
    }

    public void UpdateTokens(
        string? refreshToken,
        DateTimeOffset? refreshTokenExpires)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpires = refreshTokenExpires;

        Validate(this);
        Update(DateTimeOffset.Now);
    }

    private void Validate(User user)
    {
        if (user.Id == default) throw new Exception("მომხმარებლის იდენტიფიკატორი არავალიდურია");

        if (string.IsNullOrEmpty(user.FirstName)) throw new Exception("მომხმარებლის სახელი სავალდებულოა");

        if (string.IsNullOrEmpty(user.LastName)) throw new Exception("მომხმარებლის გვარი სავალდებულოა");

        if (string.IsNullOrEmpty(user.PhoneNumber)) throw new Exception("მომხმარებლის ტელ.ნომერი სავალდებულოა");

        if (string.IsNullOrEmpty(user.Email)) throw new Exception("მომხმარებლის ელ.ფოსტა სავალდებულოა");

        if (string.IsNullOrEmpty(user.PasswordHash)) throw new Exception("პაროლის ჰაში სავალდებულოა");

        if (string.IsNullOrEmpty(user.PasswordSalt)) throw new Exception("პაროლის გასაღები სავალდებულოა");
    }
}
