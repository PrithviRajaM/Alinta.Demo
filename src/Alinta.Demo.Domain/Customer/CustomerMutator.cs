using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alinta.Demo.Domain.Customer
{
    public class CustomerMutator
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerMutator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public DomainOutcomeCustomer CreateCustomer(Customer customer)
        {
            //do validations
            var validationResult = DoCreateCustomerValidation(customer);
            if (string.IsNullOrWhiteSpace(validationResult))
            {
                customer.Id = Guid.NewGuid();
                return new DomainOutcomeCustomer(true, "Customer created successfully.", _customerRepository.CreateCustomer(customer));
            }
            return new DomainOutcomeCustomer(false, $"Customer creation failed. Msg: {validationResult}", customer);
        }

        public DomainOutcomeCustomer UpdateCustomer(Customer customer)
        {
            //do validations
            var validationResult = DoUpdateCustomerValidation(customer);
            if (string.IsNullOrWhiteSpace(validationResult))
            {
                if (_customerRepository.UpdateCustomer(customer))
                {
                    return new DomainOutcomeCustomer(true, "Customer updated successfully.", customer);
                }
                return new DomainOutcomeCustomer(false, $"Customer update failed. Msg: The requested Customer to update is not found.", customer);
            }
            return new DomainOutcomeCustomer(false, $"Customer update failed. Msg: {validationResult}", customer);
        }

        public DomainOutcomeCustomerIds DeleteCustomer(Guid[] customerIds)
        {
            //do validations
            var validationResult = DoDeleteCustomerValidation(customerIds);
            if (string.IsNullOrWhiteSpace(validationResult))
            {
                return new DomainOutcomeCustomerIds(true, "Customer deletion successfully.", _customerRepository.DeleteCustomer(customerIds));
            }
            return new DomainOutcomeCustomerIds(false, $"Customer delete failed. Msg: {validationResult}", customerIds);
        }

        #region Validations
        private string DoCommonCustomerValidation(Customer customer)
        {
            var regex = new Regex(@"(?i)^[a-z\s]+$");
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                return $"Customer first name cannot be empty.";
            }

            if (!regex.IsMatch(customer.FirstName))
            {
                return $"Customer first name can have alphabets and spaces only.";
            }
            
            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                return $"Customer last name cannot be empty.";
            }
            
            if (!regex.IsMatch(customer.LastName))
            {
                return $"Customer last name can have alphabets and spaces only.";
            }

            if (GetAge(customer.DateOfBirth) < 0 || GetAge(customer.DateOfBirth) > 150)
            {
                return $"Provide a valid date of birth.";
            }
            
            // check whether the customer already exists
            var existingCustomerMatch = _customerRepository.GetCustomers(new CustomerFilter(null,
                customer.FirstName, customer.LastName, customer.DateOfBirth, customer.DateOfBirth));
            if (existingCustomerMatch.Any())
            {
                return $"Provide a Customer details already exists.";
            }
            return "";
        }
        private string DoCreateCustomerValidation(Customer customer)
        {
            var commonValidation = DoCommonCustomerValidation(customer);
            if (string.IsNullOrWhiteSpace(commonValidation))
            {
                // no specific create validation, as of now.
            }
            return commonValidation;
        }
        private string DoUpdateCustomerValidation(Customer customer)
        {
            var commonValidation = DoCommonCustomerValidation(customer);
            if (string.IsNullOrWhiteSpace(commonValidation))
            {
                if (customer.Id == null)
                {
                    return $"Provide a valid Customer Id to update.";
                }
                
                // check whether the customer to update exists
                var existingCustomerMatch = _customerRepository.GetCustomers(new CustomerFilter(new Guid[]{ (Guid)customer.Id },
                    null, null, null, null));
                if (!existingCustomerMatch.Any())
                {
                    return $"The customer requested for update does not exist.";
                }
            }
            return commonValidation;
        }
        private string DoDeleteCustomerValidation(Guid[] customerIds)
        {
            // check whether the customer already exists
            var existingCustomerMatch = _customerRepository.GetCustomers(new CustomerFilter(customerIds,
                null, null, null, null));
            if (existingCustomerMatch.Length != customerIds.Length)
            {
                return $"One or more provider Customer Id/s does not exist.";
            }

            return "";
        }
        #endregion
        
        private static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Now;
            var age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }
            return age;
        }
    }
}