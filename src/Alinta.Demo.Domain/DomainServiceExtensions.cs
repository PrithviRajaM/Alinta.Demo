using Alinta.Demo.Domain.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Alinta.Demo.Domain
{
    public static class DomainServiceExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddTransient<CustomerMutator>();
            services.AddTransient<CustomerQuery>();
        }
    }
}