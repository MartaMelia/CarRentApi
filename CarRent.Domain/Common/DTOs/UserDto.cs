namespace CarRent.Domain.Common.DTOs;

public class UserDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public UserRole Role { get; set; }
}
