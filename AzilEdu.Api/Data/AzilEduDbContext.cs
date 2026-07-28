using AzilEdu.Shared.Models;
using AzilEdu.Shared.Models.Animals;
using AzilEdu.Shared.Models.Donations;
using AzilEdu.Shared.Models.Donors;
using AzilEdu.Shared.Models.Employees;
using AzilEdu.Shared.Models.HousingUnits;
using AzilEdu.Shared.Models.Users;
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
    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<VolunteerStatus> VolunteerStatuses => Set<VolunteerStatus>();
    public DbSet<Donor> Donors { get; set; } = null!;
    public DbSet<DonorType> DonorTypes => Set<DonorType>();
    public DbSet<DonorStatus> DonorStatuses => Set<DonorStatus>();
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<EmployeeStatus> EmployeeStatuses => Set<EmployeeStatus>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<VolunteerTask> VolunteerTasks => Set<VolunteerTask>();
    public DbSet<VolunteerTaskStatus> VolunteerTaskStatuses => Set<VolunteerTaskStatus>();
    public DbSet<VolunteerTaskType> VolunteerTaskTypes => Set<VolunteerTaskType>();
    public DbSet<Donation> Donations { get; set; } = null!;
    public DbSet<DonationType> DonationTypes => Set<DonationType>();
    public DbSet<DonationStatus> DonationStatuses => Set<DonationStatus>();
    public DbSet<AnimalMedia> AnimalMedia => Set<AnimalMedia>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();

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

        modelBuilder.Entity<Volunteer>()
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

        modelBuilder.Entity<Donor>()
           .HasOne(donor => donor.DonorType)
           .WithMany(status => status.Donors)
           .HasForeignKey(donor => donor.DonorTypeId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonorType>().HasData(
            new DonorType { Id = 1, Name = "Fizičko osoba" },
            new DonorType { Id = 2, Name = "Tvrtka" },
            new DonorType { Id = 3, Name = "Udruga ili organizacija" }
        );

        modelBuilder.Entity<Donor>()
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

        modelBuilder.Entity<Employee>()
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

        modelBuilder.Entity<Employee>()
           .HasOne(donor => donor.EmployeeStatus)
           .WithMany(status => status.Employees)
           .HasForeignKey(donor => donor.EmployeeStatusId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeStatus>().HasData(
            new DonorType { Id = 1, Name = "Aktivan" },
            new DonorType { Id = 2, Name = "Na dopustu ili bolovanju" },
            new DonorType { Id = 3, Name = "Neaktivan" }
        );

        modelBuilder.Entity<VolunteerTask>()
            .HasOne(task => task.Volunteer)
            .WithMany()
            .HasForeignKey(task => task.VolunteerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<VolunteerTask>()
            .HasOne(task => task.Animal)
            .WithMany()
            .HasForeignKey(task => task.AnimalId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<VolunteerTask>()
            .HasOne(task => task.VolunteerTaskStatus)
            .WithMany(status => status.Tasks)
            .HasForeignKey(task => task.VolunteerTaskStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VolunteerTaskStatus>().HasData(
            new VolunteerTaskStatus { Id = 1, Name = "Otvoren" },
            new VolunteerTaskStatus { Id = 2, Name = "Dodijeljen" },
            new VolunteerTaskStatus { Id = 3, Name = "U tijeku" },
            new VolunteerTaskStatus { Id = 4, Name = "Završeno" },
            new VolunteerTaskStatus { Id = 5, Name = "Otkazano" }
        );

        modelBuilder.Entity<VolunteerTask>()
            .HasOne(task => task.VolunteerTaskType)
            .WithMany(type => type.Tasks)
            .HasForeignKey(task => task.VolunteerTaskTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<VolunteerTaskType>().HasData(
            new VolunteerTaskType { Id = 1, Name = "Šetnja" },
            new VolunteerTaskType { Id = 2, Name = "Hranjenje" },
            new VolunteerTaskType { Id = 3, Name = "Čišćenje" },
            new VolunteerTaskType { Id = 4, Name = "Socijalizacija" },
            new VolunteerTaskType { Id = 5, Name = "Prijevoz" },
            new VolunteerTaskType { Id = 6, Name = "Administracija" }
        );


        modelBuilder.Entity<Donation>()
           .HasOne(donor => donor.DonationType)
           .WithMany(status => status.Donations)
           .HasForeignKey(donor => donor.DonationTypeId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonationType>().HasData(
            new DonationType { Id = 1, Name = "Novčana" },
            new DonationType { Id = 2, Name = "Hrana" },
            new DonationType { Id = 3, Name = "Oprema" },
            new DonationType { Id = 4, Name = "Lijekovi" },
            new DonationType { Id = 5, Name = "Usluga" }
        );

       modelBuilder.Entity<Donation>()
           .HasOne(donor => donor.DonationStatus)
           .WithMany(status => status.Donations)
           .HasForeignKey(donor => donor.DonationStatusId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DonationStatus>().HasData(
            new DonationStatus { Id = 1, Name = "Evidentirana" },
            new DonationStatus { Id = 2, Name = "Potvrđena" },
            new DonationStatus { Id = 3, Name = "Iskorištena" },
            new DonationStatus { Id = 4, Name = "Otkazana" }
        );

        modelBuilder.Entity<AnimalMedia>()
            .HasOne(media => media.Animal)
            .WithMany(animal => animal.Media)
            .HasForeignKey(media => media.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AppUser>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<AppUserRole>()
            .HasKey(userRole => new { userRole.AppUserId, userRole.AppRoleId });

        modelBuilder.Entity<AppUserRole>()
            .HasOne(userRole => userRole.AppUser)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.AppUserId);

        modelBuilder.Entity<AppUserRole>()
            .HasOne(userRole => userRole.AppRole)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.AppRoleId);

        modelBuilder.Entity<AppUser>()
            .HasOne(user => user.Donor)
            .WithMany()
            .HasForeignKey(user => user.DonorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AppUser>()
            .HasOne(user => user.Volunteer)
            .WithMany()
            .HasForeignKey(user => user.VolunteerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AppUser>()
            .HasOne(user => user.Employee)
            .WithMany()
            .HasForeignKey(user => user.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AppRole>().HasData(
            new AppRole { Id = 1, Name = "User", DisplayName = "Korisnik" },
            new AppRole { Id = 2, Name = "Admin", DisplayName = "Administrator" },
            new AppRole { Id = 3, Name = "Employee", DisplayName = "Djelatnik" },
            new AppRole { Id = 4, Name = "Volunteer", DisplayName = "Volonter" },
            new AppRole { Id = 5, Name = "Donor", DisplayName = "Donator" }
        );
    }
}
