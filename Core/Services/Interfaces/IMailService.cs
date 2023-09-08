using Core.DTOs.Mail;

namespace Core.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}