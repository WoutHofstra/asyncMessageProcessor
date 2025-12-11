using System.Text.Json;
using Handlers;
using Models;

namespace Routing
{
    public class MessageRouter : IMessageRouter
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageRouter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RouteAsync(string json, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Message can not have an empty body", nameof(json));

            var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("taskType", out var taskTypeProperty))
                throw new InvalidOperationException("taskType property not found in json");

            var taskType = taskTypeProperty.GetString();
            if (string.IsNullOrWhiteSpace(taskType))
                throw new InvalidOperationException("taskType can't be null, empty or whitespace");

            switch (taskType)
            {
                case "UserWelcomeMessage":
                    await HandleAsync<UserWelcomeMessage, UserWelcomeMessageHandler>(json, cancellationToken);
                    break;

                default:
                    throw new NotSupportedException($"Task type not supported: {taskType}");
            }
        }

        private async Task HandleAsync<TMessage, THandler>(string json, CancellationToken cancellationToken)
            where TMessage: class
            where THandler: class, IMessageHandler<TMessage>
        {
            var message = JsonSerializer.Deserialize<TMessage>(json);
            if (message == null)
                throw new InvalidOperationException($"Failed to deserialize json body: {json}");

            var handler = (IMessageHandler<TMessage>)_serviceProvider.GetRequiredService(typeof(IMessageHandler<TMessage>));

            await handler.HandleAsync(message, cancellationToken);
        }
    }
}