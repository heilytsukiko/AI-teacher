namespace Backend.Services;

public class EmailService : IEmailService
{
    public Task SendConfirmationEmail(string email, string token)
    {
        // Console.WriteLine($"EMAIL to {email}: https://your-site.com/confirm?token={token}");
        return Task.CompletedTask;
    }
}

