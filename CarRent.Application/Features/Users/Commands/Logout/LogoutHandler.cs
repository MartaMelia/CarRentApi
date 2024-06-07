namespace CarRent.Application.Features.Users.Commands.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, ErrorOr<DefaultResponse>>
{
    private readonly IUserRepository _userRepository;

    public LogoutHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetSingleOrDefaultAsync(x => x.Id == request.UserId) is not User user)
            return Error.NotFound();

        user.UpdateTokens(null, null);

        _userRepository.Update(user);

        await _userRepository.SaveChanges(cancellationToken);

        return new DefaultResponse(true);
    }
}
