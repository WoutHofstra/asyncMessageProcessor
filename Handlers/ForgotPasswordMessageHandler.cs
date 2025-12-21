using Models;

namespace Handlers
{
    public class ForgotPasswordMessageHandler : IMessageHandler<ForgotPasswordMessage>
    {
        public async Task HandleAsync(ForgotPasswordMessage message, CancellationToken token)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            Console.WriteLine($"Sending forgot password message through email to {message.Username}");
        }
    }
}