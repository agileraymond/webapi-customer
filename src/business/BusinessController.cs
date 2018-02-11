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
            if (customer == null) throw new NullReferenceException("Customer object is required");
            
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                throw new ArgumentException($"{customer.FirstName} is required", customer.FirstName);
            }
        }
    }
}
