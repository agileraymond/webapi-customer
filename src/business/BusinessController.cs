using System;
using Data.Entity;

namespace Business
{
    public class BusinessController : IBusinessController
    {
        public bool AddCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            return true;
        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer == null) throw new NullReferenceException($"{nameof(Customer)} object is required");
            
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                throw new ArgumentException("FirstName is required", nameof(customer.FirstName));
            }
            if (string.IsNullOrEmpty(customer.LastName))
            {
                throw new ArgumentException("LastName is required", nameof(customer.LastName));
            }
        }
    }
}
