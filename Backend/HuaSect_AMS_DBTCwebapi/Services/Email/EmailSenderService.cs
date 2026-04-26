using HuaSect_AMS_DBTCclasslib;
using Microsoft.AspNetCore.Identity;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTC.Service;

public class EmailSenderService : IEmailSender<ApplicationUser>
{
    private readonly EmailSettings _settings;

    public EmailSenderService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var emailMsg = new MimeMessage();
        emailMsg.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        emailMsg.To.Add(MailboxAddress.Parse(user.Email ?? ""));
        emailMsg.Subject = "Email Confirmation";
        emailMsg.Body = new TextPart("html") { Text = email };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(emailMsg);
        await smtp.DisconnectAsync(true);
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        Console.WriteLine($"[DEV] Password reset link for {email}: {resetLink}");
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        Console.WriteLine($"[DEV] Password reset code for {email}: {resetCode}");
    }
}