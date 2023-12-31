﻿using System.Net.Mail;
using Core.DTOs.Mail;
using Core.Services.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ContentType = System.Net.Mime.ContentType;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Core.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.To.Add(MailboxAddress.Parse("enteremail"));
        email.Subject = mailRequest.Subject;
        
        var builder = new BodyBuilder();
        //if (mailRequest.Attachments != null)
        //{
        //    byte[] fileBytes;
        //    foreach (var file in mailRequest.Attachments)
        //    {
        //        if (file.Length > 0)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                file.CopyTo(ms);
        //                fileBytes = ms.ToArray();
        //            }
        //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //        }
        //    }
        //}
        builder.HtmlBody = mailRequest.Email+"<br/>"+mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }


}
