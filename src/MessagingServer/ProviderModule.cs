using Autofac;
using Autofac.Core;
using MessagingServer.Models;
using MessagingServer.Services;

namespace MessagingServer;

public class ProviderModule : Module
{
    private readonly AppConfig _appConfig;
    public ProviderModule(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_appConfig?.MessagingProviders?.EmailProviders is null)
        {
            return;
        }
        
        foreach (var provider in _appConfig.MessagingProviders.EmailProviders)
        {
            if (provider.Tag is null)
            {
                continue;
            }

            builder.Register<IMessagingService>(_ => new EmailMessagingService(provider))
                .Named<IMessagingService>(provider.Tag)
                .SingleInstance();
        }
    }
}