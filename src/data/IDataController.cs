using Data.Entity;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataController
    {
        int AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerId);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
    }
}