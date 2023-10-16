using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProjectSD.Interface;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Dtos;
using TestProjectSD_withDatabase.Exceptions;
using TestProjectSD_withDatabase.Extensions;

namespace TestProjectSD.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeeSetupController : ControllerBase
    {
       readonly IEmployeeRepository _employeeRepository;

        public EmployeeSetupController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

    }

    [HttpGet]
        public ActionResult<List<EmployeeDataDto>> GetEmployees()
        {
             return Ok(_employeeRepository.GetEmployees(User.Identity.GetCustomerNumber()));           
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAllBusinesLocationForEmployees(int id)
        {
            return Ok(_employeeRepository.GetAllBusinesLocationFormployee(id, User.Identity.GetCustomerNumber()));

        }

        [HttpGet]
        public ActionResult<Employee> GetSingleEmployee(int id)
        {            
            return Ok(_employeeRepository.GetSingleEmployee(id, User.Identity.GetCustomerNumber()));
        }

        [HttpPut]
        public ActionResult EditEmployee(EmployeeToEditDto employee)
        {
            try
            {
                _employeeRepository.EditEmployee(employee, User.Identity.GetCustomerNumber());
            }
            catch (IncorrectPhoneNumberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IncorrectEmailAddressException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Failed to Edit Employee {employee.FirstName} {employee.LastName}");
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult AddBusinessLocationToEmployee(int employeeId, int locationId)
        {            
            try
            {
                _employeeRepository.AddBusinessLocationToEmployee(employeeId, locationId, User.Identity.GetCustomerNumber());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmployeeToAddDto employee)
        {
            try
            {
                _employeeRepository.AddEmployee(employee, User.Identity.GetCustomerNumber());
            }
            catch (IncorrectPhoneNumberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IncorrectEmailAddressException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Failed to Add Employee {employee.FirstName} {employee.LastName}");
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteEmployee(Employee employee)
        {
            try 
            { 
                _employeeRepository.DeleteEmployee(employee, User.Identity.GetCustomerNumber());
            }
            catch(Exception)
            {
                return BadRequest($"Failed to Delete Employee {employee.FirstName} {employee.LastName}");
            }
            return Ok();            
        }
    }
}
