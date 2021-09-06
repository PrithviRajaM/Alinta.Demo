using System;

namespace Alinta.Demo.Api.GraphQL.Models
{
    public class Customer
    {
        public Customer(Guid? id, string firstName, string lastName, DateTime dateOfBirth, DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        
        public static Domain.Customer.Customer ToDomain(Customer customer)
        {
            return new Domain.Customer.Customer(customer.Id, customer.FirstName, customer.LastName, customer.DateOfBirth);
        }

        public static Customer FromDomain(Domain.Customer.Customer customer)
        {
            return new Customer(customer.Id, customer.FirstName, customer.LastName, customer.DateOfBirth, customer.CreatedAt, customer.UpdatedAt);
        }
    }
}