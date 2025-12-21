using Models;

namespace Handlers
{
    public class UserWelcomeMessageHandler : IMessageHandler<UserWelcomeMessage>
    {
        public async Task HandleAsync(UserWelcomeMessage message, CancellationToken token)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            
            Console.WriteLine($"Sending welcome email to {message.Username}");
        }
    }
}