namespace CarRent.Application.Features.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator() 
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .Equal(x => x.ConfirmPassword);
    }
}
