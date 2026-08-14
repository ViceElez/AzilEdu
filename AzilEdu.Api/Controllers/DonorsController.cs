using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs;
using AzilEdu.Shared.DTOs.Donors;
using AzilEdu.Shared.Models.Donors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize(
        Policy = AzilEdu.Api.Security.AuthorizationPolicies.Staff)]
    public class DonorsController : ControllerBase
    {
        private readonly AzilEduDbContext _context;

        public DonorsController(AzilEduDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DonorDto>>> GetDonors([FromQuery] int? statusId, [FromQuery] int? typeId)
        {
            var query = _context.Donors
                .Include(d => d.DonorType)
                .Include(d => d.DonorStatus)
                .AsQueryable();

            if (statusId.HasValue)
                query = query.Where(d => d.DonorStatusId == statusId.Value);

            if (typeId.HasValue)
                query = query.Where(d => d.DonorTypeId == typeId.Value);

            var donors = await query
                .OrderBy(d => d.FirstName)
                .ToListAsync();

            return Ok(donors.Select(ToDto).ToList());
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<List<LookupDto>>> GetDonorsLookup()
        {
            var donors = await _context.Donors
                .OrderBy(donor => donor.OrganizationName)
                .ThenBy(donor => donor.LastName)
                .ThenBy(donor => donor.FirstName)
                .Select(donor => new LookupDto
                {
                    Id = donor.Id,
                    Name = donor.OrganizationName != ""
                        ? donor.OrganizationName
                        : donor.FirstName + " " + donor.LastName
                })
                .ToListAsync();

            return Ok(donors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorDto>> GetDonorById(int id)
        {
            var donor = await _context.Donors
                .Include(item => item.DonorType)
                .Include(item => item.DonorStatus)
                .FirstOrDefaultAsync(item => item.Id == id);

            if (donor is null)
                return NotFound();

            return Ok(ToDto(donor));
        }
        [HttpPost]
        public async Task<ActionResult<DonorDto>> CreateDonor(SaveDonorDto request)
        {
            if (request.DonorTypeId <= 0 || request.DonorStatusId <= 0)
                return BadRequest("Tip i status donatora su obavezni.");

            var donor = new Donor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                OrganizationName = request.OrganizationName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                Note = request.Note,
                CreatedAt = DateTime.Now,
                DonorTypeId = request.DonorTypeId,
                DonorStatusId = request.DonorStatusId
            };

            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            await _context.Entry(donor).Reference(item => item.DonorType).LoadAsync();
            await _context.Entry(donor).Reference(item => item.DonorStatus).LoadAsync();

            return CreatedAtAction(nameof(GetDonorById), new { id = donor.Id }, ToDto(donor));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, SaveDonorDto request)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor is null)
                return NotFound();

            donor.FirstName = request.FirstName;
            donor.LastName = request.LastName;
            donor.OrganizationName = request.OrganizationName;
            donor.Email = request.Email;
            donor.PhoneNumber = request.PhoneNumber;
            donor.Address = request.Address;
            donor.City = request.City;
            donor.Note = request.Note;
            donor.DonorTypeId = request.DonorTypeId;
            donor.DonorStatusId = request.DonorStatusId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
                return NotFound();

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static DonorDto ToDto(Donor donor)
        {
            return new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                OrganizationName = donor.OrganizationName,
                DisplayName = !string.IsNullOrWhiteSpace(donor.OrganizationName)
                    ? donor.OrganizationName
                    : $"{donor.FirstName} {donor.LastName}".Trim(),
                Email = donor.Email,
                PhoneNumber = donor.PhoneNumber,
                Address = donor.Address,
                City = donor.City,
                Note = donor.Note,
                CreatedAt = donor.CreatedAt,
                DonorTypeId = donor.DonorTypeId,
                TypeName = donor.DonorType?.Name ?? string.Empty,
                DonorStatusId = donor.DonorStatusId,
                StatusName = donor.DonorStatus?.Name ?? string.Empty
            };
        }
    }
}
