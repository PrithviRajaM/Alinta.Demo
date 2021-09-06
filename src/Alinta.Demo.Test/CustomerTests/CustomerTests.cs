using System;
using Moq;
using Xunit;
using Alinta.Demo.Domain.Customer;

namespace Alinta.Demo.Test.CustomerTests
{
    public class CustomerTests
    {
        [Fact]
        public void CreateCustomerWithProperData()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithProperData);
            Assert.True(outcome.TransactionSuccessStatus);
            Assert.NotEqual(outcome.Customer.Id, Guid.Empty);
            Assert.Equal(outcome.Customer.FirstName, CustomerWithProperData.FirstName);
            Assert.Equal(outcome.Customer.LastName, CustomerWithProperData.LastName);
            Assert.Equal(outcome.Customer.DateOfBirth.Date, CustomerWithProperData.DateOfBirth);
        }
        
        [Fact]
        public void CreateCustomerWithNoFirstName()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithNoFirstName);
            Assert.False(outcome.TransactionSuccessStatus);
            Assert.Equal("Customer creation failed. Msg: Customer first name cannot be empty.", outcome.Message);
        }
        
        [Fact]
        public void CreateCustomerWithImproperFirstName()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithImproperFirstName);
            Assert.False(outcome.TransactionSuccessStatus);
            Assert.Equal("Customer creation failed. Msg: Customer first name can have alphabets and spaces only.", outcome.Message);
        }
        
        [Fact]
        public void CreateCustomerWithNoLastName()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithNoLastName);
            Assert.False(outcome.TransactionSuccessStatus);
            Assert.Equal("Customer creation failed. Msg: Customer last name cannot be empty.", outcome.Message);
        }
        
        [Fact]
        public void CreateCustomerWithImproperLastName()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithImproperLastName);
            Assert.False(outcome.TransactionSuccessStatus);
            Assert.Equal("Customer creation failed. Msg: Customer last name can have alphabets and spaces only.", outcome.Message);
        }
        
        [Fact]
        public void CreateCustomerWithOldDOB()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.CreateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => new Customer(Guid.NewGuid(), inputCustomer.FirstName, inputCustomer.LastName, inputCustomer.DateOfBirth));

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithOldDate);
            Assert.False(outcome.TransactionSuccessStatus);
            Assert.Equal("Customer creation failed. Msg: Provide a valid date of birth.", outcome.Message);
        }
        
        [Fact]
        public void UpdateCustomerWithProperData()
        {
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(repository => repository.UpdateCustomer(It.IsAny<Customer>())).Returns(
                (Customer inputCustomer) => true);
            mock.Setup(repository => repository.GetCustomers(It.IsAny<CustomerFilter>())).Returns(
                (CustomerFilter inputFilter) => new Customer[]{});

            var customerCreator = new CustomerMutator(mock.Object);
            var outcome = customerCreator.CreateCustomer(CustomerWithProperDataForUpdate);
            Assert.True(outcome.TransactionSuccessStatus);
        }
        
        #region Mock data
        
        // create customer mock data
        public Customer CustomerWithProperData = new Customer(null, "MockFirstName", "MockLastName", 
            new DateTime(2000, 05, 01));
        public Customer CustomerWithNoFirstName = new Customer(null, "", "MockLastName", 
            new DateTime(2000, 05, 01));
        public Customer CustomerWithImproperFirstName = new Customer(null, "MockFirstName123", "MockLastName", 
            new DateTime(2000, 05, 01));
        public Customer CustomerWithNoLastName = new Customer(null, "MockFirstName", "", 
            new DateTime(2000, 05, 01));
        public Customer CustomerWithImproperLastName = new Customer(null, "MockFirstName", "MockLastName456", 
            new DateTime(2000, 05, 01));
        public Customer CustomerWithOldDate = new Customer(null, "MockFirstName", "MockLastName", 
            new DateTime(1752, 05, 01));
        
        // update customer mock data
        public Customer CustomerWithProperDataForUpdate = new Customer(Guid.Parse("dc729e31-c7f0-4df1-af31-58f74a9da0f4"), 
            "MockFirstName", "MockLastName", new DateTime(2000, 05, 01));

        #endregion

    }
}