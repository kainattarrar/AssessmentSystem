namespace AssessmentSystem.Service.API.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
