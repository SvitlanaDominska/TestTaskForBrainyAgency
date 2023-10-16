using TestProjectSD.Dtos;
using TestProjectSD.Models;
using TestProjectSD_withDatabase.Dtos;

namespace TestProjectSD.Interface
{
    public interface IEmployeeRepository
    {
        public List<EmployeeDataDto> GetEmployees(int customerNumber);
        public Employee? GetSingleEmployee(int id, int customerNumber);
        public List<BusinessLocation>? GetAllBusinesLocationFormployee(int id, int customerNumber);
        public bool EditEmployee(EmployeeToEditDto employee, int customerNumber);
        public bool AddEmployee(EmployeeToAddDto employee, int customerNumber);
        public bool DeleteEmployee(Employee employee, int customerNumber);
        public bool AddBusinessLocationToEmployee(int employeeId, int locationId, int customerNumber);
    }
}
