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

        public int AddCustomer(Customer customer)
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

        public int AddAddress(Address address)
        {
            ValidateAddress(address, true);
            return _dataController.AddAddress(address);
        }

        public Address GetAddress(int addressId)
        {
            return _dataController.GetAddress(addressId).Result;
        }

        public bool DeleteAddress(int addressId)
        {
            return _dataController.DeleteAddress(addressId);
        }

        public bool UpdateAddress(Address address)
        {
            return _dataController.UpdateAddress(address);
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

        private void ValidateAddress(Address address, bool isNewAddress)
        {
            if (address == null) throw new NullReferenceException($"{nameof(Address)} object is required");        
        
            if (!isNewAddress && address.CustomerId < 1)
            {
                throw new ArgumentException($"{nameof(address.CustomerId)} must be greater than zero.");    
            }
        }

        #endregion
    }
}
