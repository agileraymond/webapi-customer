using Data.Entity;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataController
    {
        bool AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerId);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
    }
}