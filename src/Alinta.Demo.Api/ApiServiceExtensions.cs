using HotChocolate;
using Alinta.Demo.Api.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Alinta.Demo.Api
{
public static class DomainServiceExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            return services
                .AddDataLoaderRegistry()
                .AddGraphQL(
                    service => service.CreateSchema());
        }
    }
}