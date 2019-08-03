using Microsoft.Extensions.DependencyInjection;
using Provausio.SecureLink.Application;
using Provausio.SecureLink.Application.Services;

namespace Provausio.SecureLink.Api.DependencyInjection
{
    public static class ServiceInstaller
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISecuredLinkService, SecuredLinkService>();
        }
    }
}
