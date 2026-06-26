using AzilEdu.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Data;

public class AzilEduDbContext : DbContext
{
    public AzilEduDbContext(DbContextOptions<AzilEduDbContext> options) : base(options)
    {
    }
    public DbSet<HousingUnit> HousingUnits { get; set; } = null!;
    public DbSet<Animal> Animals { get; set; } = null!;
}
