using Microsoft.AspNetCore.Mvc;
using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs.Volunteers;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteersController : ControllerBase
    {
        private readonly AzilEduDbContext _context;

        public VolunteersController(AzilEduDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<VolunteerDto>>> GetVolunteers()
        {
            var volunteers = await _context.Volunteers
                .Include(volunteer => volunteer.VolunteerStatus)
                .OrderBy(volunteer => volunteer.FirstName)
                .Select(volunteer => new VolunteerDto
                {
                    Id = volunteer.Id,
                    FirstName = volunteer.FirstName,
                    LastName = volunteer.LastName,
                    Email = volunteer.Email,
                    PhoneNumber = volunteer.PhoneNumber,
                    Skills = volunteer.Skills,
                    AvailableFrom = volunteer.AvailableFrom,
                    Notes = volunteer.Notes,
                    VolunteerStatusId = volunteer.VolunteerStatusId,
                    Status = volunteer.VolunteerStatus != null ? volunteer.VolunteerStatus.Name : string.Empty,
                })
                .ToListAsync();
            return Ok(volunteers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VolunteerDto>> GetVolunteerById(int id)
        {
            var volunteer = await _context.Volunteers
                .Include(volunteer => volunteer.VolunteerStatus)
                .FirstOrDefaultAsync(volunteer => volunteer.Id == id);
            if (volunteer == null)
                return NotFound();

            var dto = new VolunteerDto
            {
                Id = volunteer.Id,
                FirstName = volunteer.FirstName,
                LastName = volunteer.LastName,
                Email = volunteer.Email,
                PhoneNumber = volunteer.PhoneNumber,
                Skills = volunteer.Skills,
                AvailableFrom = volunteer.AvailableFrom,
                Notes = volunteer.Notes,
                VolunteerStatusId = volunteer.VolunteerStatusId,
                Status = volunteer.VolunteerStatus != null ? volunteer.VolunteerStatus.Name : string.Empty,
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<VolunteerDto>> CreateVolunteer(SaveVolunteerDto createDto)
        {
            var volunteer = new Shared.Models.Volunteers.Volunteers
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                Skills = createDto.Skills,
                AvailableFrom = createDto.AvailableFrom,
                Notes = createDto.Notes,
                VolunteerStatusId = createDto.VolunteerStatusId
            };

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();

            var dto = new VolunteerDto
            {
                Id = volunteer.Id,
                FirstName = volunteer.FirstName,
                LastName = volunteer.LastName,
                Email = volunteer.Email,
                PhoneNumber = volunteer.PhoneNumber,
                Skills = volunteer.Skills,
                AvailableFrom = volunteer.AvailableFrom,
                Notes = volunteer.Notes,
                VolunteerStatusId = volunteer.VolunteerStatusId,
                Status = (await _context.VolunteerStatuses.FindAsync(volunteer.VolunteerStatusId))?.Name ?? string.Empty
            };
            return CreatedAtAction(nameof(GetVolunteerById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVolunteer(int id, SaveVolunteerDto updateDto)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
                return NotFound();

            volunteer.FirstName = updateDto.FirstName;
            volunteer.LastName = updateDto.LastName;
            volunteer.Email = updateDto.Email;
            volunteer.PhoneNumber = updateDto.PhoneNumber;
            volunteer.Skills = updateDto.Skills;
            volunteer.AvailableFrom = updateDto.AvailableFrom;
            volunteer.Notes = updateDto.Notes;
            volunteer.VolunteerStatusId = updateDto.VolunteerStatusId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteer(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
                return NotFound();

            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
