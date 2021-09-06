using System;
using Alinta.Demo.Api.GraphQL.Mutations;
using Alinta.Demo.Api.GraphQL.Queries;
using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Execution.Configuration;

namespace Alinta.Demo.Api.GraphQL
{
    public static class GraphSchemaExtensions
    {
        public static Schema CreateSchema(this IServiceProvider services)
        {
            var schema = Schema.Create(configuration =>
            {
                configuration
                    .RegisterServiceProvider(services)
                    .RegisterTypes();
                
                // Disable strict validation for purposes of debugging
                // https://hotchocolate.io/docs/general-schema-options
                configuration.Options.StrictValidation = false;
            });

#if DEBUG
            schema.MakeExecutable(new QueryExecutionOptions
            {
                IncludeExceptionDetails = true
            });
#endif
            return schema;
        }
        
        private static void RegisterTypes(this ISchemaConfiguration configuration)
        {
            configuration
                .RegisterQueryType<Query>()
                .RegisterMutationType<Mutation>();
        }
    }

}