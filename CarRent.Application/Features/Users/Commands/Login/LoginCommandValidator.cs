namespace CarRent.Application.Features.Users.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator() 
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}
