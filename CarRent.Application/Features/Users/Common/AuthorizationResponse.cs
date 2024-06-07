namespace CarRent.Application.Features.Users.Common;

public sealed record AuthorizationResponse(string AccessToken, string RefreshToken);