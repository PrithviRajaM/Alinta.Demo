using System;
using System.Collections.Generic;

namespace Alinta.Demo.Domain.Customer
{
    public interface ICustomerRepository
    {
        public Customer[] GetCustomers(CustomerFilter cFilter);

        public Customer CreateCustomer(Customer customer);
        
        bool UpdateCustomer(Customer customer);

        public Guid[] DeleteCustomer(IEnumerable<Guid> customerIds);
    }
}