using System.Collections;
using Test.Models;

namespace Test.Services
{
    public interface IEmployeeService
    {
        Task Create(EmployeeRequest employeeRequest);
        Task<IEnumerable<Employee>> GetEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> UpdateEmp(int id, EmployeeRequest employeeRequest);
        Task<Employee> DeleteEmp(int id);

    }
}
