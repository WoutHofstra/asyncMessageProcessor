using System.Threading.Tasks;
using System.Threading;
using Azure.Messaging.ServiceBus;
using Config;
using Routing;

namespace Messaging
{
    public class MessageProcessor
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSettings _settings;
        private readonly IMessageRouter _router;
        private ServiceBusProcessor? _processor;

        public MessageProcessor(ServiceBusClient client, ServiceBusSettings settings, IMessageRouter router)
        {
            _client = client;
            _settings = settings;
            _router = router;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _processor = _client.CreateProcessor(_settings.QueueName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });

            _processor.ProcessMessageAsync += HandleMessageAsync;
            _processor.ProcessErrorAsync += HandleErrorAsync;

           await _processor.StartProcessingAsync(cancellationToken);
        }

        public async Task StopAsync()
        {
            if (_processor == null)
                return;

            await _processor.StopProcessingAsync();

            _processor.ProcessMessageAsync -= HandleMessageAsync;
            _processor.ProcessErrorAsync -= HandleErrorAsync;

            await _processor.DisposeAsync();
        }

        private async Task HandleMessageAsync(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            await _router.RouteAsync(body, args.CancellationToken);

            await args.CompleteMessageAsync(args.Message);
        }

        private Task HandleErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error in the servicebus processor: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}