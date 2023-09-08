using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Mail;

public class MailRequest
{
    public static string ToEmail { get; set; } = "Mahdi.fast.g3@gmail.com";
    [Display(Name = "ایمیل")]
    public  string Email { get; set; }
    [Display(Name = "موضوع")]
    public string Subject { get; set; }
    [Display(Name = "پیام")]
    public string Body { get; set; }
    //public List<IFormFile> Attachments { get; set; }
}  
