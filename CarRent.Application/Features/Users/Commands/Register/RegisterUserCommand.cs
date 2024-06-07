namespace CarRent.Application.Features.Users.Commands.Register;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<DefaultResponse>>;