using NUnit.Framework;
using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD.Repositories;

namespace TestProject
{
    internal class EmployeeTest:DataTest
    {
        IEmployeeRepository _employeeRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _employeeRepository = new EmployeeRepository(_dbContext);
        }

        [Test]
        public void TestForGetEmployeesForCurrentUser()
        {
            var res = _employeeRepository.GetEmployees(1);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void TestForGetEmployeesForNotExistedCustomer()
        {
            var res = _employeeRepository.GetEmployees(3);

            Assert.That(res, Is.Empty);
        }

        [Test]
        public void TestForGetSingleEmployeeForCorrectCustomer()
        {
            var res = _employeeRepository.GetSingleEmployee(2, 1);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void TestForGetSingleEmployeeForInCorrectCustomer()
        {
            var res = _employeeRepository.GetSingleEmployee(2, 4);

            Assert.That(res, Is.Null);
        }

        [Test]
        public void TestForAddEmployee()
        {
            var employeeEmail = "AddedEmployee@example.com";
            var employee = new EmployeeToAddDto();
            employee.FirstName = "AddedEmployeeFirstName";
            employee.LastName = "AddedEmployeeLastName";
            employee.PhoneNumber = "012-345-6789";
            employee.Email = employeeEmail;

            _employeeRepository.AddEmployee(employee, 1);

            var exist = _dbContext.Employees.Where(e => e.Email == employeeEmail && e.Customer.CustomerNumber == 1).FirstOrDefault();
            Assert.IsNotNull(exist);
        }

        [Test]
        public void TestForAddBusnessLocationForEmployee()
        {
            _employeeRepository.AddBusinessLocationToEmployee(1, 1, 1);

            var exist = _dbContext.Employees.Where(e => e.Id == 1 && e.Customer.CustomerNumber == 1).FirstOrDefault();

            Assert.That(exist, Is.Not.Null);
            Assert.That(exist.BusinessLocations, Is.Not.Null);
            Assert.That(exist.BusinessLocations.FirstOrDefault().Id , Is.EqualTo(1));

        }

        [Test]
        public void TestForUpdateEmployee()
        {
            var email = "firstName2@example.com";
            var firstName = "FirstNameUpdated";
            var lastName = "LastNameUpdated";
            var employee = _dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();

            var employeeEdited = new EmployeeToEditDto();
            employeeEdited.FirstName = firstName;
            employeeEdited.LastName =lastName;
            employeeEdited.PhoneNumber = employee.PhoneNumber;
            employeeEdited.Id = employee.Id;
            employeeEdited.Email = employee.Email;

            _employeeRepository.EditEmployee(employeeEdited,1);

            var exist = _dbContext.Employees.Where(e => e.Email == email && e.Customer.CustomerNumber == 1).FirstOrDefault();
            Assert.IsNotNull(exist);
            Assert.That(firstName, Is.EqualTo(exist.FirstName));
            Assert.That(lastName, Is.EqualTo(exist.LastName));


        }
        [Test]
        public void TestForTryToUpdateEmployeeForAnotherCustomer()
        {
            var email = "firstName2@example.com";
            var firstName = "FirstNameUpdated";
            var lastName = "LastNameUpdated";
            var employee = _dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();

            var employeeEdited = new EmployeeToEditDto();
            employeeEdited.FirstName = firstName;
            employeeEdited.LastName = lastName;
            employeeEdited.PhoneNumber = employee.PhoneNumber;
            employeeEdited.Id = employee.Id;
            employeeEdited.Email = employee.Email;
            
            
              var res =  _employeeRepository.EditEmployee(employeeEdited, 2);

            Assert.That(res, Is.False);

        }

        [Test]
        public void TestForDeleteEmployee()
        {
            var email = "firstName1@example.com";

            var employee = _dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
            _employeeRepository.DeleteEmployee(employee, 1);

            var exist = _dbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
            Assert.That(exist, Is.Null);

        }
    }
}
