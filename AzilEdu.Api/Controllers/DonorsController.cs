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
            var donors = await _context.Donors
                .Include(d => d.DonorType)
                .Include(d => d.DonorStatus)
                .OrderBy(d => d.FirstName)
                .Select(d => new DonorDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    OrganizationName = d.OrganizationName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber,
                    Address = d.Address,
                    City = d.City,
                    Note = d.Note,
                    CreatedAt = d.CreatedAt,
                    DonorTypeId = d.DonorTypeId,
                    TypeName = d.DonorType != null ? d.DonorType.Name : string.Empty,
                    DonorStatusId = d.DonorStatusId,
                    StatusName = d.DonorStatus != null ? d.DonorStatus.Name : string.Empty
                })
                .ToListAsync();

            if (statusId.HasValue)
                donors = donors.Where(d => d.DonorStatusId == statusId.Value).ToList();

            if (typeId.HasValue)
                donors = donors.Where(d => d.DonorTypeId == typeId.Value).ToList();

            return Ok(donors);
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
                .Include(d => d.DonorType)
                .Include(d => d.DonorStatus)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donor == null)
                return NotFound();

            var dto = new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                OrganizationName = donor.OrganizationName,
                Email = donor.Email,
                PhoneNumber = donor.PhoneNumber,
                Address = donor.Address,
                City = donor.City,
                Note = donor.Note,
                CreatedAt = donor.CreatedAt,
                DonorTypeId = donor.DonorTypeId,
                TypeName = donor.DonorType != null ? donor.DonorType.Name : string.Empty,
                DonorStatusId = donor.DonorStatusId,
                StatusName = donor.DonorStatus != null ? donor.DonorStatus.Name : string.Empty
            };

            return Ok(dto);
        }


        [HttpPost]
        public async Task<ActionResult<DonorDto>> CreateDonor(SaveDonorDto createDto)
        {
            var donor = new Donor
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                OrganizationName = createDto.OrganizationName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                Address = createDto.Address,
                City = createDto.City,
                Note = createDto.Note,
                CreatedAt = DateTime.UtcNow,
                DonorTypeId = createDto.DonorTypeId,
                DonorStatusId = createDto.DonorStatusId
            };

            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            var dto = new DonorDto
            {
                Id = donor.Id,
                FirstName = donor.FirstName,
                LastName = donor.LastName,
                OrganizationName = donor.OrganizationName,
                Email = donor.Email,
                PhoneNumber = donor.PhoneNumber,
                Address = donor.Address,
                City = donor.City,
                Note = donor.Note,
                CreatedAt = donor.CreatedAt,
                DonorTypeId = donor.DonorTypeId,
                DonorStatusId = donor.DonorStatusId
            };

            return CreatedAtAction(nameof(GetDonorById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonor(int id, SaveDonorDto updateDto)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
                return NotFound();

            donor.FirstName = updateDto.FirstName;
            donor.LastName = updateDto.LastName;
            donor.OrganizationName = updateDto.OrganizationName;
            donor.Email = updateDto.Email;
            donor.PhoneNumber = updateDto.PhoneNumber;
            donor.Address = updateDto.Address;
            donor.City = updateDto.City;
            donor.Note = updateDto.Note;
            donor.DonorTypeId = updateDto.DonorTypeId;
            donor.DonorStatusId = updateDto.DonorStatusId;

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
    }
}
