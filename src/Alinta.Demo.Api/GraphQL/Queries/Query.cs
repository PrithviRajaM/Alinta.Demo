using System.Collections.Generic;
using System.Linq;
using Alinta.Demo.Domain.Customer;
using HotChocolate;
using Customer = Alinta.Demo.Api.GraphQL.Models.Customer;

namespace Alinta.Demo.Api.GraphQL.Queries
{
    /// <summary>
    /// This is the root level query object to stitch into a receiving end type, decorate the method with the Extend receiving end Type attribute
    /// </summary>
    
    public class Query
    {
        public IEnumerable<Customer> GetCustomers(CustomerFilter? customerFilter, [Service] CustomerQuery customerQuery)
        {
            return customerQuery
                .GetCustomers(customerFilter ?? new CustomerFilter())
                .Select(Customer.FromDomain);
        }
    }
}