using AzilEdu.Api.Data;
using AzilEdu.Shared.Models;
using Microsoft.EntityFrameworkCore;

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

        await db.SaveChangesAsync();
    }
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AzilEduDbContext>();

    await db.Database.MigrateAsync();

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
                IsAdopted = false,
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
                IsAdopted = true,
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
                IsAdopted = false,
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
                IsAdopted = false,
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
                IsAdopted = false,
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
                IsAdopted = true,
                ImageUrl = "/images/animals/bruno.webp",
                Description = "Udomljen pas koji ostaje u evidenciji azila."
            }
        );

        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();