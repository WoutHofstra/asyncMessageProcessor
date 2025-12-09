using System.Threading.Tasks;
using System.Threading;
using Azure.Messaging.ServiceBus;
using Config;

namespace Messaging
{
    public class MessageProcessor
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSettings _settings;
        private ServiceBusProcessor? _processor;

        public MessageProcessor(ServiceBusClient client, ServiceBusSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _processor = _client.CreateProcessor(_settings.QueueName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });

            // TODO: Add handlers to this processor when I've made them

           await _processor.StartProcessingAsync(cancellationToken);
        }

        public async Task StopAsync()
        {
            if (_processor == null)
                return;

            await _processor.StopProcessingAsync();

            // TODO: Add logic that removes handlers from the processor when i get handlers

            await _processor.DisposeAsync();
        }

        private async Task HandleMessageAsync(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            // TODO: route this message to a message router! For now we will log the message
            Console.WriteLine($"This is the message: {body}");

            await args.CompleteMessageAsync(args.Message);
        }

        private Task HandleErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error in the servicebus processor: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}