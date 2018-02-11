using Data.Entity;

namespace Data
{
    public interface IDataController
    {
        bool AddCustomer(Customer customer);
    }
}