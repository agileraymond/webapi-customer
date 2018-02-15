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
            ValidateCustomer(customer, true);
            return _dataController.AddCustomer(customer);
        }

        public Customer GetCustomer(int customerId)
        {
            return _dataController.GetCustomer(customerId).Result;    
        }

        public bool DeleteCustomer(int customerId)
        {
            return _dataController.DeleteCustomer(customerId);
        }

        public bool UpdateCustomer(Customer customer)
        {
            ValidateCustomer(customer, false);
            return _dataController.UpdateCustomer(customer);
        }

        #region private section        

        private void ValidateCustomer(Customer customer, bool isNewCustomer)
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
            if (!isNewCustomer && customer.CustomerId < 1)
            {
                throw new NullReferenceException($"{nameof(customer.CustomerId)} is required");
            }
        }

        #endregion
    }
}
