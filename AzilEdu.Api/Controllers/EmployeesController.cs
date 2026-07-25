using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs;
using AzilEdu.Shared.DTOs.Employees;
using AzilEdu.Shared.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AzilEduDbContext _context;

        public EmployeesController(AzilEduDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.EmployeePosition)
                .Include(e => e.EmployeeStatus)
                .OrderBy(e => e.FirstName)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    EmployeeNumber = e.EmployeeNumber,
                    HireDate = e.HireDate,
                    Notes = e.Notes,
                    EmployeePositionId = e.EmployeePositionId,
                    PositionName = e.EmployeePosition != null ? e.EmployeePosition.Name : string.Empty,
                    EmployeeStatusId = e.EmployeeStatusId,
                    StatusName = e.EmployeeStatus != null ? e.EmployeeStatus.Name : string.Empty
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<List<LookupDto>>> GetEmployeesLookup()
        {
            var employees = await _context.Employees
                .OrderBy(employee => employee.LastName)
                .ThenBy(employee => employee.FirstName)
                .Select(employee => new LookupDto
                {
                    Id = employee.Id,
                    Name = employee.FirstName + " " + employee.LastName
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeePosition)
                .Include(e => e.EmployeeStatus)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            var dto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeNumber = employee.EmployeeNumber,
                HireDate = employee.HireDate,
                Notes = employee.Notes,
                EmployeePositionId = employee.EmployeePositionId,
                PositionName = employee.EmployeePosition != null ? employee.EmployeePosition.Name : string.Empty,
                EmployeeStatusId = employee.EmployeeStatusId,
                StatusName = employee.EmployeeStatus != null ? employee.EmployeeStatus.Name : string.Empty
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(SaveEmployeeDto createDto)
        {
            var employee = new Employee
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                EmployeeNumber = createDto.EmployeeNumber,
                HireDate = createDto.HireDate,
                Notes = createDto.Notes,
                EmployeePositionId = createDto.EmployeePositionId,
                EmployeeStatusId = createDto.EmployeeStatusId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var dto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeNumber = employee.EmployeeNumber,
                HireDate = employee.HireDate,
                Notes = employee.Notes,
                EmployeePositionId = employee.EmployeePositionId,
                EmployeeStatusId = employee.EmployeeStatusId,
            };

            return CreatedAtAction(nameof(GetEmployeeById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, SaveEmployeeDto updateDto)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            employee.FirstName = updateDto.FirstName;
            employee.LastName = updateDto.LastName;
            employee.Email = updateDto.Email;
            employee.PhoneNumber = updateDto.PhoneNumber;
            employee.EmployeeNumber = updateDto.EmployeeNumber;
            employee.HireDate = updateDto.HireDate;
            employee.Notes = updateDto.Notes;
            employee.EmployeePositionId = updateDto.EmployeePositionId;
            employee.EmployeeStatusId = updateDto.EmployeeStatusId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
