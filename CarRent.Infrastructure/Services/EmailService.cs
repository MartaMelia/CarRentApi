namespace CarRent.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(MailSettings mailSettings)
    {
        _mailSettings = mailSettings;
    }

    public ErrorOr<bool> Send(MailRequest request, int attempt = 0) 
    {
        try
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient();

            smtpServer.Host = _mailSettings.SmtpClient;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new NetworkCredential() { UserName = _mailSettings.UserName, Password = _mailSettings.Password };
            smtpServer.Port = _mailSettings.Port;
            smtpServer.EnableSsl = true;
            smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpServer.Timeout = 20000;

            mail.From = new MailAddress(_mailSettings.From);
            mail.IsBodyHtml = true;

            foreach (string email in request.ToEmail)
            {
                mail.To.Add(email);
            }

            mail.Subject = request.Subject;
            mail.Body = request.Body;

            if (request.File != null)
            {
                request.File.Position = 0;
                Attachment attachment = new Attachment(request.File, request.FileName);
                mail.Attachments.Add(attachment);
            }

            smtpServer.Send(mail);

            return true;
        }
        catch (Exception ex)
        {
            attempt++;

            if (attempt >= _mailSettings.Attempt) return Error.Failure(code:"Mail.Sender", description: "SMTP Connection Failure");

            return Send(request, attempt);
        }
    }
}
