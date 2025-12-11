using System;
using System.Threading.Tasks;
using System.Threading;
using Models;
using Services;

namespace Handlers
{
    public class ForgotPasswordMessageHandler : IMessageHandler<ForgotPasswordMessage>
    {
        private readonly IEmailSender _emailSender;

        public ForgotPasswordMessageHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task HandleAsync(ForgotPasswordMessage message, CancellationToken token)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            string subject = "Password reset";
            string htmlBody = $"<p>Hi {message.Username}, here is your password reset token: {message.resetToken}</p>";
            string plainTextBody = $"Hi {message.Username}, here is your password reset token: {message.resetToken}";

            await _emailSender.SendEmailAsync(
                message.Email,
                subject,
                htmlBody,
                plainTextBody
            );
            
            Console.WriteLine($"Sending forgot password message through email to {message.Username}");
        }
    }
}