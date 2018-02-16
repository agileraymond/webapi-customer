using Data.Entity;

namespace Business
{
    public interface IBusinessController
    {
        int AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        bool DeleteCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
        int AddAddress(Address address);
        Address GetAddress(int addressId);
        bool DeleteAddress(int addressId);
        bool UpdateAddress(Address address);
    }
}
