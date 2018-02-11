using System;
using Data.Entity;

namespace Data
{
    public class DataController : IDataController
    {
        public bool AddCustomer(Customer customer)
        {
            return true;
        }
    }
}
