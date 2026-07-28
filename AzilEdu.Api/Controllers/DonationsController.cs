using AzilEdu.Api.Data;
using AzilEdu.Shared.DTOs.Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzilEdu.Shared.DTOs;
using AzilEdu.Shared.Models.Donations;

namespace AzilEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly AzilEduDbContext _context;

        private const int MonetaryDonationTypeId = 1;

        public DonationsController(AzilEduDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DonationDto>>> GetDonations(
            [FromQuery] int? statusId, [FromQuery] int? typeId, [FromQuery] int? donorId)
        {
            var query = _context.Donations
                .Include(d => d.DonationType)
                .Include(d => d.DonationStatus)
                .Include(d => d.Donor)
                .AsQueryable();

            if (donorId.HasValue)
                query = query.Where(d => d.DonorId == donorId.Value);

            if (statusId.HasValue)
                query = query.Where(d => d.DonationStatusId == statusId.Value);

            if (typeId.HasValue)
                query = query.Where(d => d.DonationTypeId == typeId.Value);

            var donations = await query
                .Select(d => new DonationDto
                {
                    Id = d.Id,
                    DonorId = d.DonorId,
                    DonorName = !string.IsNullOrEmpty(d.Donor.OrganizationName)
                        ? d.Donor.OrganizationName
                        : (d.Donor.FirstName + " " + d.Donor.LastName).Trim(),
                    DonationTypeId = d.DonationTypeId,
                    TypeName = d.DonationType.Name,
                    DonationStatusId = d.DonationStatusId,
                    StatusName = d.DonationStatus.Name,
                    DonationDate = d.DonationDate,
                    Amount = d.Amount,
                    ItemName = d.ItemName,
                    Quantity = d.Quantity,
                    EstimatedValue = d.EstimatedValue,
                    Notes = d.Notes
                })
                .ToListAsync();

            return Ok(donations);
        }

        [HttpGet("lookup")]
        public async Task<ActionResult<List<LookupDto>>> GetDonationsLookup()
        {
            var donations = await _context.Donations
                .OrderBy(donation => donation.DonationDate)
                .Select(donation => new LookupDto
                {
                    Id = donation.Id,
                    Name = $"{donation.DonationDate.ToShortDateString()} - {donation.ItemName} - {donation.Amount:C}"
                })
                .ToListAsync();
            return Ok(donations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonationDto>> GetDonation(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.DonationType)
                .Include(d => d.DonationStatus)
                .Select(d => new DonationDto
                {
                    Id = d.Id,
                    DonorId = d.DonorId,
                    DonationTypeId = d.DonationTypeId,
                    DonationStatusId = d.DonationStatusId,
                    DonationDate = d.DonationDate,
                    Amount = d.Amount,
                    ItemName = d.ItemName,
                    Quantity = d.Quantity,
                    EstimatedValue = d.EstimatedValue,
                    Notes = d.Notes
                })
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        [HttpPost]
        public async Task<ActionResult<DonationDto>> CreateDonation(DonationDto donationDto)
        {
            var validationError = ValidateDonation(donationDto);
            if (validationError is not null)
                return BadRequest(validationError);

            var donation = new Donation
            {
                DonorId = donationDto.DonorId,
                DonationTypeId = donationDto.DonationTypeId,
                DonationStatusId = donationDto.DonationStatusId,
                DonationDate = donationDto.DonationDate,
                Amount = donationDto.Amount,
                ItemName = donationDto.ItemName,
                Quantity = donationDto.Quantity,
                EstimatedValue = donationDto.EstimatedValue,
                Notes = donationDto.Notes
            };
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            donationDto.Id = donation.Id;
            return CreatedAtAction(nameof(GetDonation), new { id = donation.Id }, donationDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DonationDto>> UpdateDonation(int id, DonationDto donationDto)
        {
            var validationError = ValidateDonation(donationDto);
            if (validationError is not null)
                return BadRequest(validationError);

            var donation = await _context.Donations.FirstOrDefaultAsync(d => d.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            donation.DonorId = donationDto.DonorId;
            donation.DonationTypeId = donationDto.DonationTypeId;
            donation.DonationStatusId = donationDto.DonationStatusId;
            donation.DonationDate = donationDto.DonationDate;
            donation.Amount = donationDto.Amount;
            donation.ItemName = donationDto.ItemName;
            donation.Quantity = donationDto.Quantity;
            donation.EstimatedValue = donationDto.EstimatedValue;
            donation.Notes = donationDto.Notes;

            await _context.SaveChangesAsync();
            return Ok(donationDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDonation(int id)
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(d => d.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private static string? ValidateDonation(DonationDto donationDto)
        {
            if (donationDto.DonationDate.Date > DateTime.Today)
                return "Datum donacije ne smije biti u budućnosti.";

            var isMonetary = donationDto.DonationTypeId == MonetaryDonationTypeId;

            if (isMonetary)
            {
                if (donationDto.Amount <= 0)
                    return "Novčana donacija mora imati iznos veći od nule.";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(donationDto.ItemName))
                    return "Nenovčana donacija mora imati naziv.";

                if (donationDto.Quantity <= 0)
                    return "Nenovčana donacija mora imati količinu veću od nule.";
            }

            if (donationDto.Quantity < 0)
                return "Količina ne smije biti negativna.";

            if (donationDto.EstimatedValue < 0)
                return "Procijenjena vrijednost ne smije biti negativna.";

            return null;
        }
    }
}