using TestProjectSD.Interface;
using TestProjectSD.Models;

namespace TestProjectSD.Repositories
{
    public class CustomerRepository : DataRepository, ICustomerRepository
    {
       
        public CustomerRepository(DataBaseContext context):base(context)
        {
        }

        public Customer? GetSingleCustomers(string name)
        {
            return _dataContext.Customers.Where(u => u.Name == name).FirstOrDefault(); 
        }

        public bool AddCustomer(CustomerToAddDto customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name
            };

            AddEntity(newCustomer);

            return SaveChanges();           
        }

        public bool AddCustomer(string name)
        {
            var newCustomer = new Customer();
            newCustomer.Name = name;

            AddEntity(newCustomer);

            return SaveChanges();
        }
    }
}
