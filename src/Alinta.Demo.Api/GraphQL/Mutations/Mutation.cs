using System;
using Alinta.Demo.Api.GraphQL.Models;
using Alinta.Demo.Domain.Customer;
using HotChocolate;
using Customer = Alinta.Demo.Api.GraphQL.Models.Customer;

namespace Alinta.Demo.Api.GraphQL.Mutations
{
    public class Mutation
    {
        public OutcomeCustomer CreateCustomer(Customer customer, [Service] CustomerMutator customerMutator)
        {
            return Utilities.OutcomeFromDomain(customerMutator.CreateCustomer(Customer.ToDomain(customer)));
        }
        public OutcomeCustomer UpdateCustomer(Customer customer, [Service] CustomerMutator customerMutator)
        {
            return Utilities.OutcomeFromDomain(customerMutator.UpdateCustomer(Customer.ToDomain(customer)));
        }
        public OutcomeCustomerIds DeleteCustomer(Guid[] customerIds, [Service] CustomerMutator customerMutator)
        {
            return Utilities.OutcomeFromDomain(customerMutator.DeleteCustomer(customerIds));
        }
    }
}