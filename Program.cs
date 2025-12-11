using System.Security.AccessControl;
using asyncMessageProcessor;
using Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Messaging.ServiceBus;
using Azure.Communication.Email;
using Routing;
using Models;
using Handlers;
using Messaging;
using Services;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var sbSettings = new ServiceBusSettings();
        context.Configuration.GetSection("serviceBus").Bind(sbSettings);

        services.AddSingleton(sbSettings);

        services.AddSingleton<ServiceBusClient>(_ =>
            new ServiceBusClient(sbSettings.ConnectionString));

        var emailConnectionString = context.Configuration["Email:ConnectionString"];
        var emailFromAddress = context.Configuration["Email:FromAddress"];

        services.AddSingleton<IEmailSender>(sp =>
            new EmailSender(emailConnectionString = "", emailFromAddress = ""));

        services.AddSingleton<IMessageRouter, MessageRouter>();
        services.AddSingleton<MessageRouter>();
        services.AddSingleton<MessageProcessor>();

        services.AddSingleton<IMessageHandler<UserWelcomeMessage>, UserWelcomeMessageHandler>();
        services.AddSingleton<IMessageHandler<ForgotPasswordMessage>, ForgotPasswordMessageHandler>();

        services.AddHostedService<Worker>();
    })
    .Build()
    .Run();