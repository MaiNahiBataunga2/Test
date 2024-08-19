using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Services
{
    public class EmployeeServices : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task Create(EmployeeRequest employeeRequest)
        {
            if (string.IsNullOrWhiteSpace(employeeRequest.Name))
                throw new ArgumentException("Please Enter valid name.");

            if (string.IsNullOrWhiteSpace(employeeRequest.Email))
                throw new ArgumentException("Please Enter valid email.");

            var employee = new Employee
            {
                Name = employeeRequest.Name,
                Email = employeeRequest.Email

            };
            await _context.Employees.AddAsync(employee);
            _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            var response = await _context.Employees.ToListAsync();
            if (response.Count <= 0) throw new ArgumentNullException("No data present.");

            return response;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var response = await _context.Employees.FindAsync(id);
            if (response == null) throw new KeyNotFoundException("No data present with given id.");

            return response;
        }

        public async Task<Employee> UpdateEmp(int id, EmployeeRequest employeeRequest)
        {
            var response = await _context.Employees.FindAsync(id);
            if (response == null) throw new KeyNotFoundException("No data present with the given id.");

            // Update the employee's properties
            response.Name = employeeRequest.Name;
            response.Email = employeeRequest.Email;
            // Update other fields as necessary

            _context.Employees.Update(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<Employee> DeleteEmp(int id)
        {
            var response = await _context.Employees.FindAsync(id);
            if (response == null)
                throw new KeyNotFoundException("No data present with the given id.");

            _context.Employees.Remove(response);
            await _context.SaveChangesAsync();

            return response;
        }

    }
}
