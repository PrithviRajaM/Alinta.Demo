using System;
using System.Collections.Generic;
using System.Linq;
using Alinta.Demo.Domain.Customer;
using Alinta.Demo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Alinta.Demo.Infrastructure.Repositories
{
    public class DbCustomerRepository : ICustomerRepository
    {
        /// <summary>
        ///    Instantiate DB instance to have the same used below  
        /// </summary>

        private readonly DbContextOptions<LocalContext> _dbOption;
        
        public DbCustomerRepository()
        {
            _dbOption = new DbContextOptionsBuilder<LocalContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDB")
                .Options;
        }

        #region QueryActions

        public Customer[] GetCustomers(CustomerFilter cFilter)
        {
            using (var db = new LocalContext(_dbOption))
            {
                if (cFilter.CustomerIds != null && cFilter.CustomerIds.Any())
                {
                    var result = db.Customers
                        .Where(x => cFilter.CustomerIds.Contains(x.Id) && !x.IsArchived)
                        .Select(x => FromDbToDomain(x))
                        .ToArray();
                    return result;
                }
                else
                {
                    IQueryable<Schema.Customer> queryable = db.Customers
                        .Where(x => !x.IsArchived);
                    
                    if (!string.IsNullOrWhiteSpace(cFilter.FirstName))
                        queryable = queryable.Where(x => x.FirstName.ToLower().Contains(cFilter.FirstName.ToLower()));
                        
                    if (!string.IsNullOrWhiteSpace(cFilter.LastName))
                        queryable = queryable.Where(x => x.LastName.ToLower().Contains(cFilter.LastName.ToLower()));
                        
                    if (cFilter.FromDOB != null)
                        queryable = queryable.Where(x => ((DateTime)cFilter.FromDOB).Date <= x.DateOfBirth.Date);
                    
                    if (cFilter.ToDOB != null)
                        queryable = queryable.Where(x =>((DateTime)cFilter.ToDOB).Date >= x.DateOfBirth.Date);

                    var result = queryable.ToArray();
                    return result
                        .Select(FromDbToDomain)
                        .ToArray();
                }
            }
        }

        #endregion

        #region MutationActions

        public Customer CreateCustomer(Customer customer)
        {
            var dbCustomer = FromDomainToDb(customer);
            using (var db = new LocalContext(_dbOption))
            {
                db.Customers.Add(dbCustomer);
                db.SaveChanges();
            }

            return FromDbToDomain(dbCustomer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (var db = new LocalContext(_dbOption))
            {
                var dbCustomer = db.Customers.SingleOrDefault(x => x.Id == customer.Id);
                if (dbCustomer != null)
                {
                    dbCustomer.FirstName = customer.FirstName;
                    dbCustomer.LastName = customer.LastName;
                    dbCustomer.DateOfBirth = customer.DateOfBirth;
                    dbCustomer.IsArchived = false;
                    dbCustomer.UpdatedAt = DateTime.Now;
                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public Guid[] DeleteCustomer(IEnumerable<Guid> customerIds)
        {
            using (var db = new LocalContext(_dbOption))
            {
                var dbCustomer = db.Customers.Where(x => customerIds.Contains(x.Id) && !x.IsArchived).ToArray();
                foreach (var customer in dbCustomer)
                {
                    customer.IsArchived = true;
                    customer.UpdatedAt = DateTime.Now;
                }
                db.SaveChanges();
                return dbCustomer.Select(x => x.Id).ToArray();
            }
        }

        #endregion
        
        #region Utility

        public static Customer FromDbToDomain(Schema.Customer customer)
        {
            return new Customer(customer.Id, customer.FirstName, customer.LastName, customer.DateOfBirth, customer.CreatedAt, customer.UpdatedAt);
        }

        public static Schema.Customer FromDomainToDb(Customer customer)
        {
            var dbCustomer = new Schema.Customer();
            
            dbCustomer.Id = (Guid) customer.Id;
            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.DateOfBirth = customer.DateOfBirth;
            dbCustomer.CreatedAt = DateTime.Now;

            return dbCustomer;
        }


        #endregion
    }
}