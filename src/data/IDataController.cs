using Data.Entity;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataController
    {
        bool AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerId);
    }
}