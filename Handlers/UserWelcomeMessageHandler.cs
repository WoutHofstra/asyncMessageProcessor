using System;
using System.Threading.Tasks;
using System.Threading;
using Models;
using Services;

namespace Handlers
{
    public class UserWelcomeMessageHandler : IMessageHandler<UserWelcomeMessage>
    {
        private readonly IEmailSender _emailSender;

        public UserWelcomeMessageHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task HandleAsync(UserWelcomeMessage message, CancellationToken token)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            string subject = "Welcome to our platform!";
            string htmlBody = $"<p>Hi {message.Username}, welcome to our platform! We're excited to have you.</p>";
            string plainTextBody = $"Hi {message.Username}, welcome to our platform!";

            await _emailSender.SendEmailAsync(
                message.Email,
                subject,
                htmlBody,
                plainTextBody
            );
            
            Console.WriteLine($"Sending welcome email to {message.Username}");
        }
    }
}