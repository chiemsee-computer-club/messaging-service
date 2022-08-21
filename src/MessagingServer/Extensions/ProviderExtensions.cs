using System.ComponentModel;
using Autofac;
using MessagingServer.Services;

namespace MessagingServer.Extensions;

public static class ProviderExtensions
{
    public static IMessagingService GetMessagingServiceByTag(this ILifetimeScope scope, string tag)
    {
        return scope.ResolveNamed<IMessagingService>(tag);
    }
}