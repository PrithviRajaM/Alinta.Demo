using Alinta.Demo.Domain.Customer;
using Alinta.Demo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Alinta.Demo.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            AddRepositories(services);
            return services;
        }
        
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, DbCustomerRepository>();
        }
    }
}