using Microsoft.Extensions.DependencyInjection;
using Provausio.SecureLink.Api.SecuredLinks.Models;
using RiskFirst.Hateoas;

namespace Provausio.SecureLink.Api.DependencyInjection
{
    public static class HateoasInstaller
    {
        public static void AddHateoas(this IServiceCollection services)
        {
            services.AddLinks(config =>
            {
                config.UseRelativeHrefs();
                config.AddPolicy<CreateLinkResponse>(policy =>
                {
                    policy.RequireSelfLink();
                    policy.RequireRoutedLink("get", "GetSecuredData", response => new { hash = "__hash__" });
                });

                config.AddPolicy<SecuredDataResponse>(policy =>
                {
                    policy.RequireSelfLink();

                });
            });
        }
    }
}
