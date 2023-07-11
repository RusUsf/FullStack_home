using System.Reflection;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection AddMonkeyAPIServices(this IServiceCollection services)
    {
        services.AddAuthentication();

        services.AddAuthorization();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;

    }
}
