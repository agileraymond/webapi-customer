using Data.Entity;

namespace Business
{
    public interface IBusinessController
    {
        bool AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
    }
}