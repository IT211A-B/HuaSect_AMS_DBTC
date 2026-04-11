using Microsoft.AspNetCore.Identity;

namespace HuaSect_AMS_DBTC.Services;

public class NoOpEmailSender : IEmailSender<IdentityUser>
{
    public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
    {
        // Log the link for testing instead of sending email
        Console.WriteLine($"[DEV] Confirmation link for {email}: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
    {
        Console.WriteLine($"[DEV] Password reset link for {email}: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
    {
        Console.WriteLine($"[DEV] Password reset code for {email}: {resetCode}");
        return Task.CompletedTask;
    }
}