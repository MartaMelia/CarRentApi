namespace CarRent.Application.Common.Interfaces.Services;

public interface IEmailService
{
    ErrorOr<bool> Send(MailRequest request, int attempt = 0);
}
