using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize(
        Policy = AzilEdu.Api.Security.AuthorizationPolicies.Staff)]
    public class DonationTypesController: ControllerBase
    {
        private readonly AzilEduDbContext _context;
        public DonationTypesController(AzilEduDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<LookupDto>>> GetDonationTypes()
        {
            var result = await _context.DonationTypes
                .OrderBy(status => status.Name)
                .Select(status => new LookupDto
                {
                    Id = status.Id,
                    Name = status.Name
                })
                .ToListAsync();
            return Ok(result);
        }
    }
}
    