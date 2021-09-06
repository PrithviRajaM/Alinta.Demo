namespace Alinta.Demo.Domain.Customer
{
    public class CustomerQuery
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQuery(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer[] GetCustomers(CustomerFilter customerFilter)
        {
            return _customerRepository.GetCustomers(customerFilter);
        }
    }
}