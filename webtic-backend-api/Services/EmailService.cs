using System.Net.Mail;

namespace WebTIC.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            using var client = new SmtpClient
            {
                Host = smtpSettings["Server"]!,
                Port = int.Parse(smtpSettings["Port"]!),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["SenderEmail"]!, smtpSettings["SenderName"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
