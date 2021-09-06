using Alinta.Demo.Domain;
using Alinta.Demo.Infrastructure;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alinta.Demo.Api
{
    public class Startup
    {
        public Startup(){}

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApi()
                .AddInfrastructure()
                .AddDomain();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //, FimsContext db)
        {
            app.UseRouting();

            app.UseGraphQL();

            // GraphQL specific tools that allow you to query the schema.
            // Playground is for writing queries, while voyager is for exploring it visually.
            // the paths are: /voyager and /playground
            // These are exposed due to this API not being publicly accessible
            // While production release, these should be removed
            app.UsePlayground();
            app.UseVoyager();

            if (!env.IsDevelopment()) return;

            app.UseDeveloperExceptionPage();
        }
    }
}