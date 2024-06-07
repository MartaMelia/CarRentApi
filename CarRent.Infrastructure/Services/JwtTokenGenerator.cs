namespace CarRent.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public string GenerateJwtToken(User user)
    {
        var signingCredinals = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, EnumTranslator.UserRoleTranslate(user.Role)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTimeOffset.Now.AddMinutes(_jwtSettings.ExpiryMinutes).UtcDateTime,
            claims: claims,
            signingCredentials: signingCredinals);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public RefreshTokenData GenerateRefreshToken()
    {
        _ = int.TryParse(_jwtSettings.RefreshTokenValidityInDays.ToString(), out int refreshTokenValidityInDays);

        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        RefreshTokenData result = new(Convert.ToBase64String(randomNumber), DateTimeOffset.Now.AddDays(refreshTokenValidityInDays));

        return result;
    }

    public ErrorOr<ClaimsPrincipal> GetPrincipalsFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return Error.Unauthorized(code: "Authentication", description: "InvalidAccessToken");

        return principal;
    }

    public Guid GetUserIdFromToken(string token)
    {
        var principal = GetPrincipalFromToken(token);
        var claimValue = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(claimValue.Split(':').Last());
    }

    public string GetEmailFromToken(string token)
    {
        var principal = GetPrincipalFromToken(token);
        return principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
    }

    public string GetRoleFromToken(string token)
    {
        var principal = GetPrincipalFromToken(token);
        return principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;
    }

    public string GetNameFromToken(string token)
    {
        var principal = GetPrincipalFromToken(token);
        return principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        return principal;
    }
}
