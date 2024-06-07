namespace CarRent.Domain.Common.Settings.Mail;

public class MailSettings
{
    public string SmtpClient { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string From { get; set; } = null!;
    public int Port { get; set; }
    public int Attempt { get; set; }
}
