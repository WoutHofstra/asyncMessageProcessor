using System.Security.AccessControl;
using asyncMessageProcessor;
using Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Messaging.ServiceBus;
using Routing;
using Models;
using Handlers;
using Messaging;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var sbSettings = new ServiceBusSettings();
        context.Configuration.GetSection("serviceBus").Bind(sbSettings);

        services.AddSingleton(sbSettings);

        services.AddSingleton<ServiceBusClient>(_ =>
            new ServiceBusClient(sbSettings.ConnectionString));

        services.AddSingleton<MessageRouter>();
        
        services.AddSingleton<MessageProcessor>();

        services.AddScoped<IMessageHandler<UserWelcomeMessage>, UserWelcomeMessageHandler>();

        services.AddHostedService<Worker>();
    })
    .Build()
    .Run();