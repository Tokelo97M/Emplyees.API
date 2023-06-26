using Emplyees.API.Data;
using Emplyees.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emplyees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _employeesDbContext;

        public EmployeesController(EmployeeDbContext employeesDbContext)
        {
            _employeesDbContext = employeesDbContext;
        }

        //Returns all employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeesDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        //Add employees
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.id = Guid.NewGuid();

            await _employeesDbContext.Employees.AddAsync(employeeRequest);
            await _employeesDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee =
            await _employeesDbContext.Employees.FirstOrDefaultAsync(x => x.id == id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _employeesDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;   
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            await _employeesDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeesDbContext.Employees.FindAsync(id);

            if(id == null)
            {
                return NotFound();
            }

            _employeesDbContext.Employees.Remove(employee);
            await _employeesDbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}
