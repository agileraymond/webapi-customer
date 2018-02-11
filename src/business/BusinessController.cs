using System;
using Data.Entity;
using Data;

namespace Business
{
    public class BusinessController : IBusinessController
    {
        private readonly IDataController _dataController;

        public BusinessController(IDataController dataController)
        {
            _dataController = dataController;
        }

        public bool AddCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            return _dataController.AddCustomer(customer);
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
