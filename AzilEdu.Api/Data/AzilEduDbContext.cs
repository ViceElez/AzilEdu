using AzilEdu.Shared.Models;
using AzilEdu.Shared.Models.Animals;
using AzilEdu.Shared.Models.Donors;
using AzilEdu.Shared.Models.Employees;
using AzilEdu.Shared.Models.HousingUnits;
using AzilEdu.Shared.Models.Volunteers;
using Microsoft.EntityFrameworkCore;

namespace AzilEdu.Api.Data;

public class AzilEduDbContext : DbContext
{
    public AzilEduDbContext(DbContextOptions<AzilEduDbContext> options) : base(options)
    {
    }
    public DbSet<HousingUnit> HousingUnits { get; set; } = null!;
    public DbSet<Animal> Animals { get; set; } = null!;
    public DbSet<AnimalStatus> AnimalStatuses => Set<AnimalStatus>();
    public DbSet<Volunteers> Volunteers { get; set; } = null!;
    public DbSet<VolunteerStatus> VolunteerStatuses => Set<VolunteerStatus>();
    public DbSet<Donors> Donors { get; set; } = null!;
    public DbSet<DonorType> DonorTypes => Set<DonorType>();
    public DbSet<DonorStatus> DonorStatuses => Set<DonorStatus>();
    public DbSet<Employees> Employees { get; set; } = null!;
    public DbSet<EmployeeStatus> EmployeeStatuses => Set<EmployeeStatus>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Animal>()
            .HasOne(animal => animal.AnimalStatus)
            .WithMany(status => status.Animals)
            .HasForeignKey(animal => animal.AnimalStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AnimalStatus>().HasData(
            new AnimalStatus { Id = 1, Name = "Dostupna za udomljenje" },
            new AnimalStatus { Id = 2, Name = "Rezervirana" },
            new AnimalStatus { Id = 3, Name = "Udomljena" },
            new AnimalStatus { Id = 4, Name = "Na liječenju" }
        );

        modelBuilder.Entity<Volunteers>()
            .HasOne(volunteer=>volunteer.VolunteerStatus)
            .WithMany(status => status.Volunteers)
            .HasForeignKey(volunteer => volunteer.VolunteerStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VolunteerStatus>().HasData(
            new VolunteerStatus { Id = 1, Name = "Novi" },
            new VolunteerStatus { Id = 2, Name = "Aktivan" },
            new VolunteerStatus { Id = 3, Name = "Privremeno nedostupan " },
            new VolunteerStatus { Id = 4, Name = "Neaktivan" }
        );

        modelBuilder.Entity<Donors>()
           .HasOne(donor => donor.DonorType)
           .WithMany(status => status.Donors)
           .HasForeignKey(donor => donor.DonorTypeId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonorType>().HasData(
            new DonorType { Id = 1, Name = "Fizičko osoba" },
            new DonorType { Id = 2, Name = "Tvrtka" },
            new DonorType { Id = 3, Name = "Udruga ili organizacija" }
        );

        modelBuilder.Entity<Donors>()
            .HasOne(donor => donor.DonorStatus)
            .WithMany(status => status.Donors)
            .HasForeignKey(donor => donor.DonorStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonorStatus>().HasData(
            new DonorStatus { Id = 1, Name = "Novi" },
            new DonorStatus { Id = 2, Name = "Aktivan" },
            new DonorStatus { Id = 3, Name = "Povremeni" },
            new DonorStatus { Id = 4, Name = "Neaktivan" }
        );

        modelBuilder.Entity<Employees>()
            .HasOne(donor => donor.EmployeePosition)
            .WithMany(status => status.Employees)
            .HasForeignKey(donor => donor.EmployeePositionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeePosition>().HasData(
            new DonorStatus { Id = 1, Name = "Djelatnik azila" },
            new DonorStatus { Id = 2, Name = "Veterinar" },
            new DonorStatus { Id = 3, Name = "Koordinator volontera" },
            new DonorStatus { Id = 4, Name = "Administrator" }
        );

        modelBuilder.Entity<Employees>()
           .HasOne(donor => donor.EmployeeStatus)
           .WithMany(status => status.Employees)
           .HasForeignKey(donor => donor.EmployeeStatusId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeStatus>().HasData(
            new DonorType { Id = 1, Name = "Aktivan" },
            new DonorType { Id = 2, Name = "Na dopustu ili bolovanju" },
            new DonorType { Id = 3, Name = "Neaktivan" }
        );
    }
}
