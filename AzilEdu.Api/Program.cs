using AzilEdu.Api.Data;
using AzilEdu.Shared.Models.HousingUnits;
using Microsoft.EntityFrameworkCore;
using AzilEdu.Shared.Models.Animals;
using AzilEdu.Shared.Models.Volunteers;
using AzilEdu.Shared.Models.Employees;
using AzilEdu.Shared.Models.Donors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AzilEduDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AzilEduDbContext>();

    await db.Database.MigrateAsync();

    if (!await db.HousingUnits.AnyAsync())
    {
        db.HousingUnits.AddRange(
            new HousingUnit
            {
                Name = "Box 1",
                UnitType = "Boks za pse",
                Capacity = 4,
                Occupied = 2,
                LastCleanedAt = DateTime.Now.AddDays(-2),
                IsActive = true,
                ImageUrl = "/images/housing-units/box-1.webp",
                Note = "Dva psa trenutno"
            },
            new HousingUnit
            {
                Name = "Box 2",
                UnitType = "Boks za pse",
                Capacity = 6,
                Occupied = 6,
                LastCleanedAt = DateTime.Now.AddDays(-1),
                IsActive = true,
                ImageUrl = "/images/housing-units/box-2.webp",
                Note = "Puna jedinica"
            },
            new HousingUnit
            {
                Name = "Prostor za mačke",
                UnitType = "Prostor za mačke",
                Capacity = 8,
                Occupied = 3,
                LastCleanedAt = DateTime.Now.AddHours(-6),
                IsActive = true,
                ImageUrl = "/images/housing-units/macke-prostor.webp",
                Note = "Prostor s platformama"
            },
            new HousingUnit
            {
                Name = "Karantena 1",
                UnitType = "Karantena",
                Capacity = 2,
                Occupied = 1,
                LastCleanedAt = DateTime.Now,
                IsActive = true,
                ImageUrl = "/images/housing-units/karantena.webp",
                Note = "Izolirani prostor"
            },
            new HousingUnit
            {
                Name = "Box 3",
                UnitType = "Boks za pse",
                Capacity = 5,
                Occupied = 0,
                LastCleanedAt = DateTime.Now.AddMonths(-1),
                IsActive = false,
                ImageUrl = "/images/housing-units/box-3.webp",
                Note = "Trenutno nije u upotrebi"
            },
            new HousingUnit
            {
                Name = "Privremeni prostor",
                UnitType = "Boks za pse",
                Capacity = 3,
                Occupied = 1,
                LastCleanedAt = null,
                IsActive = true,
                ImageUrl = "/images/housing-units/privremeni.webp",
                Note = "Nema podatka o čišćenju"
            },
            new HousingUnit
            {
                Name = "Box 4",
                UnitType = "Boks za pse",
                Capacity = 4,
                Occupied = 4,
                LastCleanedAt = DateTime.Now.AddHours(-12),
                IsActive = true,
                ImageUrl = "/images/housing-units/box-4.webp",
                Note = "Na kapacitetu"
            },
            new HousingUnit
            {
                Name = "Box 5",
                UnitType = "Boks za pse",
                Capacity = 5,
                Occupied = 1,
                LastCleanedAt = DateTime.Now.AddDays(-3),
                IsActive = true,
                ImageUrl = "/images/housing-units/box-5.webp",
                Note = "Slobodno 4 mjesta"
            },
            new HousingUnit
            {
                Name = "Prostor za nove životinje",
                UnitType = "Prostor za mačke",
                Capacity = 6,
                Occupied = 2,
                LastCleanedAt = DateTime.Now.AddDays(-1),
                IsActive = true,
                ImageUrl = "/images/housing-units/novi-prostor.webp",
                Note = "Prostor za nove dolaske"
            },
            new HousingUnit
            {
                Name = "Box za promatranje",
                UnitType = "Boks za pse",
                Capacity = 2,
                Occupied = 0,
                LastCleanedAt = DateTime.Now.AddHours(-2),
                IsActive = true,
                ImageUrl = "/images/housing-units/opservacija.webp",
                Note = "Rezervan za nove pse"
            },
            new HousingUnit
            {
                Name = "Karantena 2",
                UnitType = "Karantena",
                Capacity = 3,
                Occupied = 2,
                LastCleanedAt = null,
                IsActive = true,
                ImageUrl = "/images/housing-units/karantena-2.webp",
                Note = "Sekundarna karantena"
            }
        );
    }

    if (!await db.Animals.AnyAsync())
    {
        db.Animals.AddRange(
            new Animal
            {
                Name = "Luna",
                Species = "Pas",
                Breed = "Labrador",
                Gender = "Ženka",
                Age = 3,
                ArrivalDate = new DateTime(2025, 10, 12),
                AnimalStatusId = 1,
                ImageUrl = "/images/animals/luna.webp",
                Description = "Mirna i druželjubiva kujica koja voli šetnje."
            },
            new Animal
            {
                Name = "Maza",
                Species = "Mačka",
                Breed = "Domaća kratkodlaka",
                Gender = "Ženka",
                Age = 2,
                ArrivalDate = new DateTime(2025, 11, 5),
                AnimalStatusId = 3,
                ImageUrl = "/images/animals/maza.webp",
                Description = "Zaigrana mačka naviknuta na boravak u zatvorenom prostoru."
            },
            new Animal
            {
                Name = "Rex",
                Species = "Pas",
                Breed = "Njemački ovčar",
                Gender = "Mužjak",
                Age = 5,
                ArrivalDate = new DateTime(2026, 1, 20),
                AnimalStatusId = 1,
                ImageUrl = "/images/animals/rex.webp",
                Description = "Aktivan pas koji traži iskusnijeg vlasnika."
            },
            new Animal
            {
                Name = "Nala",
                Species = "Mačka",
                Breed = "Maine Coon mješanac",
                Gender = "Ženka",
                Age = null,
                ArrivalDate = new DateTime(2026, 2, 3),
                AnimalStatusId = 1,
                ImageUrl = "/images/animals/nala.webp",
                Description = "Mlada mačka pronađena bez poznate povijesti."
            },
            new Animal
            {
                Name = "Tobi",
                Species = "Pas",
                Breed = "Mješanac",
                Gender = "Mužjak",
                Age = 1,
                ArrivalDate = null,
                AnimalStatusId = 2,
                ImageUrl = "/images/animals/tobi.webp",
                Description = "Vesel pas kojem datum dolaska još nije potvrđen."
            },
            new Animal
            {
                Name = "Bruno",
                Species = "Pas",
                Breed = "Bigl",
                Gender = "Mužjak",
                Age = 4,
                ArrivalDate = new DateTime(2025, 9, 18),
                AnimalStatusId = 3,
                ImageUrl = "/images/animals/bruno.webp",
                Description = "Udomljen pas koji ostaje u evidenciji azila."
            }
        );
    }

    if (!await db.Volunteers.AnyAsync())
    {
        db.Volunteers.AddRange(
            new Volunteer
            {
                FirstName = "Ivan",
                LastName = "Horvat",
                Email = "ivan.horvat@example.com",
                PhoneNumber = "0912345678",
                Skills = "Šetnja pasa, čišćenje",
                AvailableFrom = "Ponedjeljak, Srijeda",
                Notes = "Iskusan s velikim psima",
                VolunteerStatusId = 1
            },
            new Volunteer
            {
                FirstName = "Ana",
                LastName = "Kovač",
                Email = "ana.kovac@example.com",
                PhoneNumber = "0987654321",
                Skills = "Rad s mačkama",
                AvailableFrom = "Vikendom",
                Notes = "Nova volonterka",
                VolunteerStatusId = 2
            },
            new Volunteer
            {
                FirstName = "Marko",
                LastName = "Novak",
                Email = "marko.novak@example.com",
                PhoneNumber = "0951122334",
                Skills = "Prijevoz, održavanje",
                AvailableFrom = "Fleksibilno",
                Notes = "Ima kombi",
                VolunteerStatusId = 3
            },
            new Volunteer
            {
                FirstName = "Petra",
                LastName = "Jurić",
                Email = "petra.juric@example.com",
                PhoneNumber = "0998877665",
                Skills = "Administracija",
                AvailableFrom = "Radnim danom prijepodne",
                Notes = "Pomaže na mrežama",
                VolunteerStatusId = 4
            }
        );
    }

    if (!await db.Employees.AnyAsync())
    {
        db.Employees.AddRange(
            new Employee
            {
                FirstName = "Luka",
                LastName = "Babić",
                Email = "luka.babic@example.com",
                PhoneNumber = "0911122334",
                EmployeeNumber = 1001,
                HireDate = new DateTime(2022, 5, 10),
                Notes = "Voditelj skloništa",
                EmployeePositionId = 1,
                EmployeeStatusId = 1
            },
            new Employee
            {
                FirstName = "Maja",
                LastName = "Knez",
                Email = "maja.knez@example.com",
                PhoneNumber = "0982233445",
                EmployeeNumber = 1002,
                HireDate = new DateTime(2023, 8, 15),
                Notes = "Veterinarski tehničar",
                EmployeePositionId = 2,
                EmployeeStatusId = 1
            },
            new Employee
            {
                FirstName = "Stjepan",
                LastName = "Radić",
                Email = "stjepan.radic@example.com",
                PhoneNumber = "0953344556",
                EmployeeNumber = 1003,
                HireDate = new DateTime(2024, 1, 20),
                Notes = "Tim za udomljavanje",
                EmployeePositionId = 3,
                EmployeeStatusId = 2
            }
        );
    }

    if (!await db.Donors.AnyAsync())
    {
        db.Donors.AddRange(
            new Donor
            {
                FirstName = "Ivana",
                LastName = "Matić",
                OrganizationName = "",
                Email = "ivana.matic@example.com",
                PhoneNumber = "0915566778",
                Address = "Ilica 10",
                City = "Zagreb",
                Note = "Redoviti donator hrane",
                CreatedAt = DateTime.UtcNow.AddMonths(-5),
                DonorTypeId = 1,
                DonorStatusId = 1
            },
            new Donor
            {
                FirstName = "Josip",
                LastName = "Vuković",
                OrganizationName = "PetShop HR",
                Email = "kontakt@petshop.hr",
                PhoneNumber = "012345678",
                Address = "Vukovarska 20",
                City = "Split",
                Note = "Donacija opreme",
                CreatedAt = DateTime.UtcNow.AddYears(-1),
                DonorTypeId = 2,
                DonorStatusId = 1
            },
            new Donor
            {
                FirstName = "Katarina",
                LastName = "Lovrić",
                OrganizationName = "",
                Email = "katarina.lovric@example.com",
                PhoneNumber = "0998877665",
                Address = "Zagrebačka 5",
                City = "Rijeka",
                Note = "Jednokratna financijska donacija",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                DonorTypeId = 1,
                DonorStatusId = 2
            }
        );
    }

    await db.SaveChangesAsync();

    await db.SaveChangesAsync();
    await AppUserSeeder.SeedAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();