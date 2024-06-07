namespace CarRent.Application.Features.Users.Commands.Logout;

public sealed record LogoutCommand(Guid UserId) : IRequest<ErrorOr<DefaultResponse>>;