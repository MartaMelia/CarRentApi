namespace CarRent.Application.Features.Users.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ErrorOr<DefaultResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _emailService = emailService;
    }

    public async Task<ErrorOr<DefaultResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.AnyAsync(x => x.PhoneNumber == request.PhoneNumber.Trim()))
            return Error.Conflict(code:"Phone Number", description: "Phone Number should be unique");

        if (await _userRepository.AnyAsync(x => x.Email.ToLower() == request.Email.Trim().ToLower()))
            return Error.Conflict(code: "Email", description: "Email should be unique");

        string passwordHash = _passwordService.HashPasword(request.Password, out var passwordSalt);

        User user = new(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            request.Email,
            passwordHash,
            Convert.ToHexString(passwordSalt),
            UserRole.Member,
            null,
            null);

        _userRepository.Create(user);

        await _userRepository.SaveChanges(cancellationToken);

        MailRequest email = new(
            new List<string>() { request.Email},
            "Successfull Registration",
            "<h1>Welcome to Car-Rent!</h1>",
            null,
            null);

        var emailResponse = _emailService.Send(email);

        if (emailResponse.IsError) return emailResponse.Errors;

        return new DefaultResponse(emailResponse.Value);
    }
}
