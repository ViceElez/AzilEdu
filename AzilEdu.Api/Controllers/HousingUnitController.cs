using AzilEdu.Api.Data;
using AzilEdu.Shared.Models;
using AzilEdu.Shared.Models.HousingUnits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HousingUnitController : ControllerBase
{
    private readonly AzilEduDbContext _context;

    public HousingUnitController(AzilEduDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<HousingUnit>>> GetHousingUnits()
    {
        var housingUnits = await _context.HousingUnits
            .OrderBy(h => h.Name)
            .Select(h => new HousingUnitDto
            {
                Id = h.Id,
                Name = h.Name,
                UnitType = h.UnitType,
                Capacity = h.Capacity,
                Occupied = h.Occupied,
                LastCleanedAt = h.LastCleanedAt,
                IsActive = h.IsActive,
                ImageUrl = h.ImageUrl,
                Note = h.Note
            })
            .ToListAsync();

        return Ok(housingUnits);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HousingUnitDto>> GetHousingUnitById(int id)
    {
        var housingUnit = await _context.HousingUnits.FindAsync(id);
        if (housingUnit == null)
            return null;

        var housingUnitDto = new HousingUnitDto
        {
            Id = housingUnit.Id,
            Name = housingUnit.Name,
            UnitType = housingUnit.UnitType,
            Capacity = housingUnit.Capacity,
            Occupied = housingUnit.Occupied,
            LastCleanedAt = housingUnit.LastCleanedAt,
            IsActive = housingUnit.IsActive,
            ImageUrl = housingUnit.ImageUrl,
            Note = housingUnit.Note
        };

        return Ok(housingUnitDto);
    }

    [HttpPost]
    public async Task<ActionResult<HousingUnitDto>> CreateHousingUnit(HousingUnitDto housingUnitDto)
    {
        var housingUnit = new HousingUnit
        {
            Name = housingUnitDto.Name,
            UnitType = housingUnitDto.UnitType,
            Capacity = housingUnitDto.Capacity,
            Occupied = housingUnitDto.Occupied,
            LastCleanedAt = housingUnitDto.LastCleanedAt,
            IsActive = housingUnitDto.IsActive,
            ImageUrl = housingUnitDto.ImageUrl,
            Note = housingUnitDto.Note
        };

        _context.HousingUnits.Add(housingUnit);
        await _context.SaveChangesAsync();

        var result = new HousingUnitDto
        {
            Name = housingUnitDto.Name,
            UnitType = housingUnitDto.UnitType,
            Capacity = housingUnitDto.Capacity,
            Occupied = housingUnitDto.Occupied,
            LastCleanedAt = housingUnitDto.LastCleanedAt,
            IsActive = housingUnitDto.IsActive,
            ImageUrl = housingUnitDto.ImageUrl,
            Note = housingUnitDto.Note
        };

        return CreatedAtAction(nameof(GetHousingUnitById), new { id = housingUnit.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<HousingUnitDto>> UpdateHousingUnit(int id, HousingUnitDto housingUnitDto)
    {
        var housingUnit = await _context.HousingUnits.FindAsync(id);
        if (housingUnit == null)
            return NotFound();

        housingUnit.Name = housingUnitDto.Name;
        housingUnit.UnitType = housingUnitDto.UnitType;
        housingUnit.Capacity = housingUnitDto.Capacity;
        housingUnit.Occupied = housingUnitDto.Occupied;
        housingUnit.LastCleanedAt = housingUnitDto.LastCleanedAt;
        housingUnit.IsActive = housingUnitDto.IsActive;
        housingUnit.ImageUrl = housingUnitDto.ImageUrl;
        housingUnit.Note = housingUnitDto.Note;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHousingUnit(int id)
    {
        var housingUnit = await _context.HousingUnits.FindAsync(id);
        if (housingUnit == null)
            return null;

        _context.HousingUnits.Remove(housingUnit);
        await _context.SaveChangesAsync();

        return NoContent();


    }
}