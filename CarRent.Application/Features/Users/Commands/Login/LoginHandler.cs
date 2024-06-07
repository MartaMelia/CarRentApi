namespace CarRent.Application.Features.Users.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ErrorOr<AuthorizationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthorizationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetSingleOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber.Trim()) is not User user)
            return Error.Validation(code:"Invalid", description:"Phone Number or Password is incorrect");

        if (user.IsDeleted) return Error.Forbidden();

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash, Convert.FromHexString(user.PasswordSalt)))
            return Error.Validation(code: "Invalid", description: "Phone Number or Password is incorrect");

        string accessToken = _jwtTokenGenerator.GenerateJwtToken(user);

        RefreshTokenData refreshTokenData = _jwtTokenGenerator.GenerateRefreshToken();

        user.UpdateTokens(refreshTokenData.RefreshToken, refreshTokenData.Expires);

        _userRepository.Update(user);

        await _userRepository.SaveChanges(cancellationToken);

        return new AuthorizationResponse(accessToken, refreshTokenData.RefreshToken);
    }
}
