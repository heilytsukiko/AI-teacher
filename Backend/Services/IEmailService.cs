namespace Backend.Services;

public interface IEmailService
{
    Task SendConfirmationEmail(string email, string token);
}
