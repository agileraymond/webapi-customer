using System;
using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataController : IDataController
    {
        private readonly CustomerDbContext _customerDbContext;
        public DataController(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;        
        }

        public bool AddCustomer(Customer customer)
        {
            _customerDbContext.Customers.Add(customer);
            return _customerDbContext.SaveChanges() > 0;            
        }
    }
}
