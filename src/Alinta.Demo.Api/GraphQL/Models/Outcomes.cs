using System;

namespace Alinta.Demo.Api.GraphQL.Models
{
    public class Outcomes
    {
        public bool TransactionSuccessStatus { get; set; }
        public string Message { get; set; } = null!;
    }

    public class OutcomeCustomer : Outcomes
    {
        public Customer Customer { get; set; }
        
        public OutcomeCustomer(bool transactionSuccessStatus, string message, Customer customer)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            Customer = customer;
        }
    }
    
    public class OutcomeCustomers : Outcomes
    {
        public Customer[] Customers { get; set; }
        
        public OutcomeCustomers(bool transactionSuccessStatus, string message, Customer[] customers)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            Customers = customers;
        }
    }
    
    public class OutcomeCustomerIds : Outcomes
    {
        public Guid[] CustomerIds { get; set; }
        
        public OutcomeCustomerIds(bool transactionSuccessStatus, string message, Guid[] customerIds)
        {
            TransactionSuccessStatus = transactionSuccessStatus;
            Message = message;
            CustomerIds = customerIds;
        }
    }
}