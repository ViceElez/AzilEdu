# AzilEdu

## Pokretanje aplikacije

Unutar solutiona treba desni klik i pod opcijom **Configure Startup Projects** odabrati `AzilEdu.App` i `AzilEdu.Api`.

Nakon namještanja projekta, aplikacija se pokreće klikom na botun **Start**.

Preko terminala, pokretanje projekta ide ovako:

```bash
cd AzilEdu.Api
# pokreni migracije
dotnet run

cd ..
cd AzilEdu.App
dotnet run
```

---

## Korisnici za navigaciju aplikacije

| Email | Lozinka | Uloge |
|---|---|---|
| admin@aziledu.local | Admin123! | User, Admin |
| employee@aziledu.local | Employee123! | User, Employee |
| volunteer@aziledu.local | Volunteer123! | User, Volunteer |
| donor@aziledu.local | Donor123! | User, Donor |

---

## Opis relacija

### `AppUser` – `AppRole`

- **Tip relacije:** many-to-many
- **Opis:** Jedan `AppUser` može imati više uloga (npr. istovremeno "Employee" i "Admin"), a jedna `AppRole` može biti dodijeljena više korisnika. Realizirano preko join tablice `AppUserRole` (ili `IdentityUserRole<TKey>` ako se koristi ugrađeni ASP.NET Core Identity model), koja sadrži `AppUserId` i `AppRoleId` kao kompozitni ključ / vanjske ključeve.
- **Napomena:** Autorizacijske politike (`AuthorizationPolicies.Staff`, `AuthorizationPolicies.AdminOnly`) provjeravaju je li korisnik u **jednoj od** dozvoljenih uloga preko `RequireRole(...)`, ne zahtijevaju točno jednu ulogu.

### `AppUser` – `Volunteer`

- **Tip relacije:** one-to-one
- **Opis:** Svaki `AppUser` s ulogom "Volunteer" povezan je s točno jednim `Volunteer` zapisom (kroz FK, npr. `Volunteer.AppUserId`). Taj zapis sadrži podatke specifične za volontera (vještine, dostupnost, status) koji se ne nalaze u samom `AppUser` entitetu.

### `AppUser` – `Donor`

- **Tip relacije:** one-to-one
- **Opis:** Svaki `AppUser` s ulogom "Donor" povezan je s točno jednim `Donor` zapisom (kroz FK, npr. `Donor.AppUserId`). Sadrži podatke specifične za donatora (organizacija, adresa, tip donatora, status).

### `AppUser` – `Employee`

- **Tip relacije:** one-to-one
- **Opis:** Svaki `AppUser` s ulogom "Employee" povezan je s točno jednim `Employee` zapisom (kroz FK, npr. `Employee.AppUserId`). Sadrži podatke specifične za djelatnika (broj djelatnika, datum zapošljavanja, pozicija, status).

---

## 8 testnih endpointa

| # | Endpoint | Korisnik | Opis | Status |
|---|---|---|---|---|
| 1 | `GET /api/animals` | Admin | Dohvati sve životinje | 200 |
| 2 | `POST /api/animals/create` | Admin | Kreiraj životinju | 201 |
| 3 | `PUT /api/animals/1` | Admin | Pregled pojedinačne životinje | 204 |
| 4 | `GET /api/animals` | Bez tokena | Pokušaj pregleda životinja | 401 |
| 5 | `GET /api/animals` | Volunteer | Pokušaj pregleda životinja | 403 |
| 6 | `GET /api/my-tasks` | Volunteer 1 | Dohvati samo svoje zadatke | 200 |
| 7 | `GET /api/volunteer-tasks` | Employee 1 | Dohvat volonterski zadataka | 200 |
| 8 | `GET /api/my-donations` | Donor 1 | Dohvati samo svoje donacije | 200 |
