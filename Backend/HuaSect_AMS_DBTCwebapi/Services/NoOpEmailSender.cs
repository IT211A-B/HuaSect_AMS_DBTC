using HuaSect_AMS_DBTCclasslib;
using Microsoft.AspNetCore.Identity;

namespace HuaSect_AMS_DBTC.Service;

public class NoOpEmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        // Log the link for testing instead of sending email
        Console.WriteLine($"[DEV] Confirmation link for {email}: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        Console.WriteLine($"[DEV] Password reset link for {email}: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        Console.WriteLine($"[DEV] Password reset code for {email}: {resetCode}");
        return Task.CompletedTask;
    }
}