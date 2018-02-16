using System;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data
{
    public class DataController : IDataController
    {
        private readonly CustomerDbContext _customerDbContext;
        public DataController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;        
        }

        public int AddCustomer(Customer customer)
        {
            _customerDbContext.Customers.Add(customer);
            _customerDbContext.SaveChanges();
            return customer.CustomerId;            
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await _customerDbContext.Customers.SingleAsync(b => b.CustomerId == customerId); 
        }
        
        public bool DeleteCustomer(int customerId)
        {
            var customer = new Customer { CustomerId = customerId };
            _customerDbContext.Customers.Remove(customer);
            return _customerDbContext.SaveChanges() > 0;            
        } 
        
        public bool UpdateCustomer(Customer customer)
        {
            _customerDbContext.Customers.Update(customer);
            return _customerDbContext.SaveChanges() > 0;    
        }

        public int AddAddress(Address address)
        {
            _customerDbContext.Addresses.Add(address);
            _customerDbContext.SaveChanges();
            return address.AddressId;
        }

        public async Task<Address> GetAddress(int addressId)
        {
            return await _customerDbContext.Addresses.SingleAsync(b => b.AddressId == addressId);
        }

        public bool DeleteAddress(int addressId)
        {
            var address = new Address { AddressId = addressId };
            _customerDbContext.Addresses.Remove(address);
            return _customerDbContext.SaveChanges() > 0;
        }

        public bool UpdateAddress(Address address)
        {
            _customerDbContext.Addresses.Update(address);
            return _customerDbContext.SaveChanges() > 0;
        }
    }
}
