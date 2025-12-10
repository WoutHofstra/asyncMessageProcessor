using Messaging;

namespace asyncMessageProcessor;

public class Worker : BackgroundService
{
    private readonly MessageProcessor _messageProcessor;

    public Worker(MessageProcessor messageProcessor)
    {
        _messageProcessor = messageProcessor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageProcessor.StartAsync(stoppingToken);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            
        }

        await _messageProcessor.StopAsync();
    }   
}
