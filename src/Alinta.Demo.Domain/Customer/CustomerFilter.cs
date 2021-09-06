using System;

namespace Alinta.Demo.Domain.Customer
{
    public class CustomerFilter
    {
        public CustomerFilter()
        {
            CustomerIds = new Guid[]{};
            FirstName = "";
            LastName = "";
            FromDOB = null;
            ToDOB = null;
        }
        public CustomerFilter(Guid[]? customerIds, string? firstName, string? lastName, DateTime? fromDob, DateTime? toDob)
        {
            CustomerIds = customerIds ?? new Guid[]{};
            FirstName = firstName ?? "";
            LastName = lastName ?? "";
            FromDOB = fromDob ?? null;
            ToDOB = toDob ?? null;
        }
        public Guid[]? CustomerIds { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? FromDOB { get; set; }
        public DateTime? ToDOB { get; set; }
    }
}