using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize(
        Policy = AzilEdu.Api.Security.AuthorizationPolicies.AdminOnly)]
    public class EmployeesPositionsController : ControllerBase
    {
        private readonly AzilEduDbContext _context;

        public EmployeesPositionsController(AzilEduDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<LookupDto>>> GetEmployeesPositions()
        {
            var result = await _context.EmployeePositions
                .OrderBy(position => position.Name)
                .Select(position => new LookupDto
                {
                    Id = position.Id,
                    Name = position.Name
                })
                .ToListAsync();
            return Ok(result);
        }
    }
}
