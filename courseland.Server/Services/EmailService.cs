using MailKit.Net.Smtp;
using MimeKit;

namespace courseland.Server.Services
{
    public class EmailService
    {

        private ILogger<EmailService> logger;
        private IConfiguration configuration;
        
        public MailboxAddress Mail { get; set; }

        public EmailService(ILogger<EmailService> logger, MailboxAddress mail, IConfiguration configuration)
        {
            this.logger = logger;
            Mail = mail;
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(String to, String subject, String message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(Mail);
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync(configuration["Address"], configuration["Password"]);
                await client.SendAsync(emailMessage);

                logger.LogInformation("done.");

                await client.DisconnectAsync(true);
            }
        }
    }
}
