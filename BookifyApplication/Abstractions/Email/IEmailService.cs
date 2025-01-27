namespace BookifyApplication.Abstractions.Email;
public interface IEmailService
{
    Task SensAsync(Bookify.Domain.Users.Email recipient, string subject, string body);
}
