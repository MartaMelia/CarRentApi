namespace CarRent.Domain.Common.Models;

public record RefreshTokenData(string RefreshToken, DateTimeOffset Expires);
