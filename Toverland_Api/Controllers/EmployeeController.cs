using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Toverland_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Area)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Role,
                    e.AreaId,
                    AreaName = e.Area.Name,
                    e.Email,
                    e.PhoneNumber,
                    e.HireDate,
                    e.IsActive
                })
                .ToListAsync();

            return Ok(employees);
        }


        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([Bind("Name,Role,AreaId,Email,PhoneNumber,HireDate,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, [Bind("Name,Role,AreaId,Email,PhoneNumber,HireDate,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = await _context.Employees.FindAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.Name = employee.Name;
                existingEmployee.Role = employee.Role;
                existingEmployee.AreaId = employee.AreaId;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.IsActive = employee.IsActive;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

