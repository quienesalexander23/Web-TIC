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
            var server = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "smtp.gmail.com";
            var port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
            var username = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            var senderName = Environment.GetEnvironmentVariable("SMTP_SENDER_NAME") ?? "WebTIC EPN";
            var senderEmail = Environment.GetEnvironmentVariable("SMTP_SENDER_EMAIL") ?? username;

            using var client = new SmtpClient
            {
                Host = server,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(username, password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail!, senderName),
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
