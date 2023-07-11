using System.Reflection;
using MediatR;
using Serilog.Ui.Web.Authorization;

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

public class CustomAuthorizeFilter : IUiAuthorizationFilter
{
    public bool Authorize(HttpContext httpContext)
    {
        return httpContext.User.Identity is { IsAuthenticated: true };
    }
}
