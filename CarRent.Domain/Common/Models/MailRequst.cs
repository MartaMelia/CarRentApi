namespace CarRent.Domain.Common.Models;

public sealed record MailRequest(
    IList<string> ToEmail,
    string Subject,
    string Body,
    MemoryStream? File,
    string? FileName);

