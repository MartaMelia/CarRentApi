namespace CarRent.Application.Features.Users.Commands.Login;

public sealed record LoginCommand(string PhoneNumber, string Password) : IRequest<ErrorOr<AuthorizationResponse>>;