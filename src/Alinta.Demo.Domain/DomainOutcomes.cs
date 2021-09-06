using System;

namespace Alinta.Demo.Domain
{
    public class DomainOutcomes
    {
        public bool TransactionSuccessStatus { get; set; }
        public string Message { get; set; } = null!;
    }

    public class DomainOutcomeCustomer : DomainOutcomes
    {
        public Customer.Customer Customer { get; set; }

        public DomainOutcomeCustomer(bool transactionSuccessStatus, string message, Customer.Customer customer)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            Customer = customer;
        }
    }
    
    public class DomainOutcomeCustomers : DomainOutcomes
    {
        public Customer.Customer[] Customers { get; set; }
        
        public DomainOutcomeCustomers(bool transactionSuccessStatus, string message, Customer.Customer[] customers)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            Customers = customers;
        }
    }
    
    public class DomainOutcomeCustomerIds : DomainOutcomes
    {
        public Guid[] CustomerIds { get; set; }
        
        public DomainOutcomeCustomerIds(bool transactionSuccessStatus, string message, Guid[] customerIds)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            CustomerIds = customerIds;
        }

    }
}