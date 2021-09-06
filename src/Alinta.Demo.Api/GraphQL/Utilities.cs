using System.Linq;
using Alinta.Demo.Api.GraphQL.Models;
using Alinta.Demo.Domain;

namespace Alinta.Demo.Api.GraphQL
{
    public class Utilities
    {
        public static OutcomeCustomer OutcomeFromDomain(DomainOutcomeCustomer domainOutcome)
        {
            return new OutcomeCustomer(domainOutcome.TransactionSuccessStatus, domainOutcome.Message, Customer.FromDomain(domainOutcome.Customer));
        }
        public static OutcomeCustomers OutcomeFromDomain(DomainOutcomeCustomers domainOutcome)
        {
            return new OutcomeCustomers(domainOutcome.TransactionSuccessStatus, domainOutcome.Message, 
                domainOutcome.Customers.Select(Customer.FromDomain).ToArray());
        }
        public static OutcomeCustomerIds OutcomeFromDomain(DomainOutcomeCustomerIds domainOutcome)
        {
            return new OutcomeCustomerIds(domainOutcome.TransactionSuccessStatus, domainOutcome.Message, domainOutcome.CustomerIds);
        }
    }
}