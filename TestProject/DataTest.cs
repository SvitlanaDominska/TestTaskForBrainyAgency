using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSD.Interface;
using TestProjectSD.Repositories;
using TestProjectSD;
using TestProjectSD.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TestProject
{
    internal class DataTest
    {
        protected DataBaseContext _dbContext;
        private DbContextOptions<DataBaseContext> _contextOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _contextOptions = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase("InMemoryDataTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            _dbContext = new DataBaseContext(_contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            CreateData();

            _dbContext.SaveChanges();     
          
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _dbContext.Database.EnsureDeleted();
        }

        private void CreateData()
        {
            var Customer1 = new Customer
            {
                CustomerNumber = 1,
                Name = "Customer1"
            };

            var customers = new List<Customer>
            {
                Customer1,
                new Customer
                {
                    CustomerNumber = 2,
                    Name = "Customer2"
                },
                new Customer
                {
                    CustomerNumber = 3,
                    Name = "CustomerForDelete"
                }
            };

            _dbContext.Customers.AddRange(customers);

            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "FirstName1",
                    LastName = "LastName1",
                    Email = "firstName1@example.com",
                    PhoneNumber = "0123456789",
                    Customer = Customer1

                },
                new Employee
                {
                     Id = 2,
                    FirstName = "FirstName2",
                    LastName = "LastName2",
                    Email = "firstName2@example.com",
                    PhoneNumber = "012-345-6789",
                    Customer = Customer1
                }
            };
            _dbContext.Employees.AddRange(employees);
            var busnessLocation = new List<BusinessLocation>()
            {
                new BusinessLocation
                {
                    Id = 1,
                    Name = "Location1",
                    Address = "LocationAddress",
                    PhoneNumber = "0123456789",
                    CustomerNumber = 1
                },
                new BusinessLocation
                {
                    Id = 2,
                    Name = "Location2",
                    Address = "LocationAddress2",
                    PhoneNumber = "0123456789",
                    CustomerNumber = 1
                }
            };
            _dbContext.BusinessLocations.AddRange(busnessLocation);

            _dbContext.SaveChanges();
        }

    }
}
