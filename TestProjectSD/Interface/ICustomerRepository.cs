using TestProjectSD.Models;

namespace TestProjectSD.Interface
{
    public interface ICustomerRepository 
    {
        public Customer? GetSingleCustomers(string name);
        public bool AddCustomer(string name);
        public bool AddCustomer(CustomerToAddDto customer);
    }
}
