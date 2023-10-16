using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Dtos;
using TestProjectSD_withDatabase.Helpers;

namespace TestProjectSD.Repositories
{
    public class EmployeeRepository : DataRepository, IEmployeeRepository
    {

        public EmployeeRepository(DataBaseContext context):base(context)
        {
        }
        public List<EmployeeDataDto> GetEmployees(int customerNumber)
        {
            var data =  _dataContext.Employees.Include(b => b.BusinessLocations).Where(x => x.Customer.CustomerNumber == customerNumber).ToList();
            var employees = new List<EmployeeDataDto>();
            foreach (var employee in data)
            {
                var emp = new EmployeeDataDto();
                emp.Id = employee.Id;
                emp.Email = employee.Email;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.PhoneNumber = employee.PhoneNumber;
                var businessLocations = new Collection<BusinessLocationDataDto>();
                foreach (var location in employee.BusinessLocations)
                { 
                    var businessLocation = new BusinessLocationDataDto();
                    businessLocation.Address = location.Address;
                    businessLocation.Name = location.Name;
                    businessLocation.PhoneNumber = location.PhoneNumber;
                    businessLocation.Id = location.Id;
                    businessLocations.Add(businessLocation);
                }
                emp.BusinessLocations = businessLocations;
                employees.Add(emp);
            }
            return employees;
        }

        public List<BusinessLocation>? GetAllBusinesLocationFormployee(int id, int customerNumber)
        {
            return _dataContext.Employees.Include(c => c.BusinessLocations).Where(x => x.Id == id && x.Customer.CustomerNumber == customerNumber).FirstOrDefault()?.BusinessLocations.ToList();
        }

        public Employee? GetSingleEmployee(int id, int customerNumber)
        {
            return  _dataContext.Employees.Where(x => x.Id == id && x.Customer.CustomerNumber == customerNumber).FirstOrDefault();
          
        }

        public bool EditEmployee(EmployeeToEditDto employee, int customerNumber)
        {
           
            var employeeDb = _dataContext.Employees.Where(x => x.Id == employee.Id && x.Customer.CustomerNumber == customerNumber).FirstOrDefault();

            if (employeeDb != null)
            {
                employeeDb.FirstName = employee.FirstName;
                employeeDb.LastName = employee.LastName;
                employeeDb.PhoneNumber = PhoneNumber.VerifyPhoneNumber(employee.PhoneNumber);
                employeeDb.Email = EmailAddress.VerifyEmailAddress(employee.Email);

                return SaveChanges();               
            }
            return false;
        }

        public bool AddBusinessLocationToEmployee(int employeeId, int locationId, int customerNumber)
        {

            var employeeDb = _dataContext.Employees.Include(b=>b.BusinessLocations).Where(x => x.Id == employeeId && x.Customer.CustomerNumber == customerNumber).FirstOrDefault() ?? throw new Exception($"Employee with Id {employeeId} doesn't exists");
            var locationDb = _dataContext.BusinessLocations.Where(x => x.Id == locationId && x.CustomerNumber == customerNumber).FirstOrDefault() ?? throw new Exception($"Business Location with Id {locationId} doesn't exists for current customer");

            if (employeeDb.BusinessLocations != null && employeeDb.BusinessLocations.Where(b => b.Id == locationDb.Id).Any())
            {
                throw new Exception("This location already added to this employee");
            }

            if (employeeDb.BusinessLocations != null)
            {
                employeeDb.BusinessLocations.Add(locationDb);
            }
            else
            {
                var businesLocation = new List<BusinessLocation>() { locationDb };

            }
            return SaveChanges();
        }

        public bool AddEmployee(EmployeeToAddDto employee, int customerNumber)
        {
           var newEmployee = new Employee();

            newEmployee.FirstName = employee.FirstName;
            newEmployee.LastName = employee.LastName;
            newEmployee.PhoneNumber = PhoneNumber.VerifyPhoneNumber(employee.PhoneNumber);
            newEmployee.Email = EmailAddress.VerifyEmailAddress(employee.Email);

            var customerDb = _dataContext.Customers.Where(x => x.CustomerNumber == customerNumber).FirstOrDefault() ?? throw new Exception($"Customer with Id {customerNumber} doesn't exists");

            newEmployee.Customer = customerDb;

            AddEntity(newEmployee);

            return SaveChanges();           
        }

        public bool DeleteEmployee(Employee employee, int customerNumber)
        {
           var employeeDb = _dataContext.Employees.Where(x => x.Id == employee.Id && x.Customer.CustomerNumber == customerNumber).FirstOrDefault()?? throw new Exception($"You can't delete employee {employee.FirstName} {employee.LastName}"); ;

            if (employeeDb != null)
            {
                RemoveEntity(employeeDb);
                return SaveChanges();
            }
            return false;
        }

    }
}
