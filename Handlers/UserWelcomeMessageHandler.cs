using System;
using System.Threading.Tasks;
using System.Threading;
using Models;

namespace Handlers
{
    public class UserWelcomeMessageHandler : IMessageHandler<UserWelcomeMessage>
    {
        public Task HandleAsync(UserWelcomeMessage message, CancellationToken token)
        {
            // In a real world setting, this would send a real email, or call a function that does so
            
            Console.WriteLine($"Sending welcome email to {message.Username}");
            return Task.CompletedTask;
        }
    }
}