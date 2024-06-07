namespace CarRent.Application.Common.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
    RefreshTokenData GenerateRefreshToken();
    ErrorOr<ClaimsPrincipal> GetPrincipalsFromExpiredToken(string token);
    Guid GetUserIdFromToken(string token);
    string GetEmailFromToken(string token);
    string GetRoleFromToken(string token);
    string GetNameFromToken(string token);
}
