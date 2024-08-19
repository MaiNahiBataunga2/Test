using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employee;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employee = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employee.GetEmployee();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpById(int id)
        {
            var response = await _employee.GetEmployeeById(id);
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequest employeeRequest)
        {
            await _employee.Create(employeeRequest);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmp(int id, EmployeeRequest employeeRequest)
        {
            if (id <= 0)
                return BadRequest("Invalid employee ID.");

            try
            {
                var updatedEmployee = await _employee.UpdateEmp(id, employeeRequest);
                return Ok(updatedEmployee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Employee with id {id} not found.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmp(int id) 
        {
            if (id <= 0) return BadRequest("Invalid Employee ID.");

            try
            {
                var deleteEmp =  await _employee.DeleteEmp(id);
                return Ok($"Employee with id {id} has been deleted successfully.");
            }
            catch (KeyNotFoundException)
            {

                return NotFound($"Employee with id {id} not found.");
                    
            }
        }


    }

}
