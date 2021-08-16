namespace CarShopBg.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using CarShopBg.Data;
    using CarShopBg.Data.Models;
    using static Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedAdministrator(services);
            SeedCategories(services);
            SeedBrands(services);
            SeedModels(services);



            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarShopBgDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@csbg.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarShopBgDbContext>();
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "Cabriolet"},
                new Category{Name = "Touring"},
                new Category{Name = "Sedan"},
                new Category{Name = "Hatchback"},
                new Category{Name = "Coupe"},
                new Category{Name = "SUV"},
                new Category{Name = "Van"}

            });
            data.SaveChanges();
        }

        private static void SeedBrands(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarShopBgDbContext>();

            if (data.Brands.Any())
            {
                return;
            }

            data.Brands.AddRange(new[]
            {
                new Brand{Name = "Toyota"},
                new Brand{Name = "Honda"},
                new Brand{Name = "Chevrolet"},
                new Brand{Name = "Mercedes-Benz"},
                new Brand{Name = "Jeep"},
                new Brand{Name = "BMW"},
                new Brand{Name = "Porsche"},
                new Brand{Name = "Subaru"},
                new Brand{Name = "Nissan"},
                new Brand{Name = "Cadillac"},
                new Brand{Name = "Volkswagen"},
                new Brand{Name = "Lexus"},
                new Brand{Name = "Audi"},
                new Brand{Name = "Ferrari"},
                new Brand{Name = "Volvo"},
                new Brand{Name = "Jaguar"},
                new Brand{Name = "GMC"},
                new Brand{Name = "Acura"},
                new Brand{Name = "Bentley"},
                new Brand{Name = "Dodge"},
                new Brand{Name = "Hyundai"},
                new Brand{Name = "Lincoln"},
                new Brand{Name = "Mazda"},
                new Brand{Name = "Land Rover"},
                new Brand{Name = "Tesla"},
                new Brand{Name = "Kia"},
                new Brand{Name = "Chrysler"},
                new Brand{Name = "Infiniti"},
                new Brand{Name = "Mitsubishi"},
                new Brand{Name = "Maserati"},
                new Brand{Name = "Bugatti"},
                new Brand{Name = "Fiat"},
                new Brand{Name = "Mini"},
                new Brand{Name = "Alfa Romeo"},
                new Brand{Name = "Saab"},
                new Brand{Name = "Genesis"},
                new Brand{Name = "Suzuki"},
                new Brand{Name = "Renault"},
                new Brand{Name = "Peugeot"},
                new Brand{Name = "Daewoo"},
                new Brand{Name = "Citroen"},
                new Brand{Name = "MG"}
            });
            data.Brands.OrderBy(b => b.Name);

            data.SaveChanges();
        }

        private static void SeedModels(IServiceProvider services)
        {
            var data = services.GetRequiredService<CarShopBgDbContext>();

            if (data.Models.Any())
            {
                return;
            }

            data.Models.AddRange(new[]
            {
                new Model{Name = "Avalon", BrandId = 28},
                new Model{Name = "Corolla", BrandId = 28},
                new Model{Name = "Prius", BrandId = 28},
                new Model{Name = "Yaris", BrandId = 28},
                new Model{Name = "RAV4", BrandId = 28},
                new Model{Name = "Highlander", BrandId = 28},
                new Model{Name = "4Runner", BrandId = 28},
                new Model{Name = "Land Cruiser", BrandId = 28},
                new Model{Name = "C-HR", BrandId = 28},
                new Model{Name = "Tundra", BrandId = 28},
                new Model{Name = "CR-V", BrandId = 27},
                new Model{Name = "HR-V", BrandId = 27},
                new Model{Name = "Passport", BrandId = 27},
                new Model{Name = "Accord", BrandId = 27},
                new Model{Name = "Civic", BrandId = 27},
                new Model{Name = "Clarity", BrandId = 27},
                new Model{Name = "Jazz", BrandId = 27},
                new Model{Name = "Legend", BrandId = 27},
                new Model{Name = "Aveo", BrandId = 26},
                new Model{Name = "Captiva", BrandId = 26},
                new Model{Name = "Cruze", BrandId = 26},
                new Model{Name = "Orlando", BrandId = 26},
                new Model{Name = "Spark", BrandId = 26},
                new Model{Name = "Camaro", BrandId = 26},
                new Model{Name = "Corvette", BrandId = 26},
                new Model{Name = "Volt", BrandId = 26},
                new Model{Name = "A-Class", BrandId = 25},
                new Model{Name = "B-Class", BrandId = 25},
                new Model{Name = "C-Class", BrandId = 25},
                new Model{Name = "E-Class", BrandId = 25},
                new Model{Name = "S-Class", BrandId = 25},
                new Model{Name = "G-Class", BrandId = 25},
                new Model{Name = "GLA", BrandId = 25},
                new Model{Name = "GLC", BrandId = 25},
                new Model{Name = "GLE", BrandId = 25},
                new Model{Name = "GLS", BrandId = 25},
                new Model{Name = "GL", BrandId = 25 },
                new Model{Name = "CLK", BrandId = 25},
                new Model{Name = "CLS", BrandId = 25},
                new Model{Name = "CLA", BrandId = 25},
                new Model{Name = "SL", BrandId = 25},
                new Model{Name = "Gladiator", BrandId = 24},
                new Model{Name = "Wrangler", BrandId = 24},
                new Model{Name = "Cherokee", BrandId = 24},
                new Model{Name = "Grand Cherokee", BrandId = 24},
                new Model{Name = "Compass", BrandId = 24},
                new Model{Name = "Renegade", BrandId = 24},
                new Model{Name = "Wrangler JK", BrandId = 24},
                new Model{Name = "1-Series", BrandId = 23},
                new Model{Name = "2-Series", BrandId = 23},
                new Model{Name = "3-Series" ,BrandId = 23},
                new Model{Name = "4-Series", BrandId = 23},
                new Model{Name = "5-Series", BrandId = 23},
                new Model{Name = "6-Series", BrandId = 23},
                new Model{Name = "7-Series", BrandId = 23},
                new Model{Name = "X1", BrandId = 23},
                new Model{Name = "X2", BrandId = 23},
                new Model{Name = "X3", BrandId = 23},
                new Model{Name = "X4", BrandId = 23},
                new Model{Name = "X5", BrandId = 23},
                new Model{Name = "X6", BrandId = 23},
                new Model{Name = "X7", BrandId = 23},
                new Model{Name = "Z4", BrandId = 23},
                new Model{Name = "i3", BrandId = 23},
                new Model{Name = "i8", BrandId = 23},
                new Model{Name = "Avenger", BrandId = 22},
                new Model{Name = "Caliber", BrandId = 22},
                new Model{Name = "Caravan", BrandId = 22},
                new Model{Name = "Dakota", BrandId = 22},
                new Model{Name = "Dart", BrandId = 22},
                new Model{Name = "Grand Caravan", BrandId = 22},
                new Model{Name = "Journey", BrandId = 22},
                new Model{Name = "Charger", BrandId = 22},
                new Model{Name = "Challenger", BrandId = 22},
                new Model{Name = "i10", BrandId = 21},
                new Model{Name = "i20", BrandId = 21},
                new Model{Name = "i30", BrandId = 21},
                new Model{Name = "H-1", BrandId = 21},
                new Model{Name = "Staria", BrandId = 21},
                new Model{Name = "Kona", BrandId = 21},
                new Model{Name = "Santa Fe", BrandId = 21},
                new Model{Name = "IONIQ", BrandId = 21},
                new Model{Name = "NEXO", BrandId = 21},
                new Model{Name = "Continental", BrandId = 20},
                new Model{Name = "Navicross", BrandId = 20},
                new Model{Name = "Mark X", BrandId = 20},
                new Model{Name = "MKR", BrandId = 20},
                new Model{Name = "C", BrandId = 20},
                new Model{Name = "MKZ", BrandId = 20},
                new Model{Name = "MKC", BrandId = 20},
                new Model{Name = "Corsair", BrandId = 20},
                new Model{Name = "Navigator", BrandId = 20},
                new Model{Name = "Aviator", BrandId = 20},
                new Model{Name = "2", BrandId = 19},
                new Model{Name = "3", BrandId = 19},
                new Model{Name = "6", BrandId = 19},
                new Model{Name = "CX-3", BrandId = 19},
                new Model{Name = "CX-5", BrandId = 19},
                new Model{Name = "CX-8", BrandId = 19},
                new Model{Name = "CX-9", BrandId = 19},
                new Model{Name = "MX-5", BrandId = 19},
                new Model{Name = "CX-30", BrandId = 19},
                new Model{Name = "MX-30", BrandId = 19},
                new Model{Name = "BT-50", BrandId = 19},
                new Model{Name = "Tosca", BrandId = 18},
                new Model{Name = "Damas II", BrandId = 18},
                new Model{Name = "Winstorm", BrandId = 18},
                new Model{Name = "Lacetti", BrandId = 18},
                new Model{Name = "Gentra", BrandId = 18},
                new Model{Name = "Matiz", BrandId = 18},
                new Model{Name = "G2X", BrandId = 18},
                new Model{Name = "206", BrandId = 17},
                new Model{Name = "207", BrandId = 17},
                new Model{Name = "208", BrandId = 17},
                new Model{Name = "305", BrandId = 17},
                new Model{Name = "306", BrandId = 17},
                new Model{Name = "307", BrandId = 17},
                new Model{Name = "308", BrandId = 17},
                new Model{Name = "405", BrandId = 17},
                new Model{Name = "406", BrandId = 17},
                new Model{Name = "407", BrandId = 17},
                new Model{Name = "508", BrandId = 17},
                new Model{Name = "605", BrandId = 17},
                new Model{Name = "607", BrandId = 17},
                new Model{Name = "2008", BrandId = 17},
                new Model{Name = "3008", BrandId = 17},
                new Model{Name = "5008", BrandId = 17},
                new Model{Name = "Partner", BrandId = 17},
                new Model{Name = "Boxer", BrandId = 17},
                new Model{Name = "Expert", BrandId = 17},
                new Model{Name = "Megane", BrandId = 16},
                new Model{Name = "Twingo", BrandId = 16},
                new Model{Name = "Symbol", BrandId = 16},
                new Model{Name = "Scenic", BrandId = 16},
                new Model{Name = "Twizy", BrandId = 16},
                new Model{Name = "Zoe", BrandId = 16},
                new Model{Name = "Clio", BrandId = 16},
                new Model{Name = "Vitara", BrandId = 15},
                new Model{Name = "Grand Vitara", BrandId = 15},
                new Model{Name = "SX-4", BrandId = 15},
                new Model{Name = "Swift", BrandId = 15},
                new Model{Name = "CV1", BrandId = 15},
                new Model{Name = "Jimny", BrandId = 15},
                new Model{Name = "Kizashi", BrandId = 15},
                new Model{Name = "Liana", BrandId = 15},
                new Model{Name = "Nomade", BrandId = 15},
                new Model{Name = "XL6", BrandId = 15},
                new Model{Name = "G70", BrandId = 14},
                new Model{Name = "G80", BrandId = 14},
                new Model{Name = "G90", BrandId = 14},
                new Model{Name = "GV70", BrandId = 14},
                new Model{Name = "GV80", BrandId = 14},
                new Model{Name = "9-5", BrandId = 13},
                new Model{Name = "9-3", BrandId = 13},
                new Model{Name = "900", BrandId = 13},
                new Model{Name = "600", BrandId = 13},
                new Model{Name = "9-4X", BrandId = 13},
                new Model{Name = "9-7X", BrandId = 13},
                new Model{Name = "9-2X", BrandId = 13},
                new Model{Name = "9000", BrandId = 13},
                new Model{Name = "90", BrandId = 13},
                new Model{Name = "145", BrandId = 12},
                new Model{Name = "146", BrandId = 12},
                new Model{Name = "147", BrandId = 12},
                new Model{Name = "155", BrandId = 12},
                new Model{Name = "156", BrandId = 12},
                new Model{Name = "159", BrandId = 12},
                new Model{Name = "164", BrandId = 12},
                new Model{Name = "166", BrandId = 12},
                new Model{Name = "Giulietta", BrandId = 12},
                new Model{Name = "Giulia", BrandId = 12},
                new Model{Name = "GT", BrandId = 12},
                new Model{Name = "GTA", BrandId = 12},
                new Model{Name = "MiTo", BrandId = 12},
                new Model{Name = "Stelvio", BrandId = 12},
                new Model{Name = "Cooper", BrandId = 11},
                new Model{Name = "Convertible", BrandId = 11},
                new Model{Name = "Clubman", BrandId = 11},
                new Model{Name = "Coupé", BrandId = 11},
                new Model{Name = "Countryman", BrandId = 11},
                new Model{Name = "Clubman", BrandId = 11},
                new Model{Name = "Bravo", BrandId = 10},
                new Model{Name = "Punto", BrandId = 10},
                new Model{Name = "Linea", BrandId = 10},
                new Model{Name = "Coupé", BrandId = 10},
                new Model{Name = "Tipo", BrandId = 10},
                new Model{Name = "Croma", BrandId = 10},
                new Model{Name = "Panda", BrandId = 10},
                new Model{Name = "Multipla", BrandId = 10},
                new Model{Name = "Sedici", BrandId = 10},
                new Model{Name = "500", BrandId = 10},
                new Model{Name = "Chiron", BrandId = 9},
                new Model{Name = "Veyron", BrandId = 9},
                new Model{Name = "EB110", BrandId = 9},
                new Model{Name = "MC20", BrandId = 8},
                new Model{Name = "Quattroporte", BrandId = 8},
                new Model{Name = "Ghibli", BrandId = 8},
                new Model{Name = "GranTurismo", BrandId = 8},
                new Model{Name = "GranSport", BrandId = 8},
                new Model{Name = "Spyder", BrandId = 8},
                new Model{Name = "Coupé", BrandId = 8},
                new Model{Name = "3200 GTA", BrandId = 8},
                new Model{Name = "3000GT", BrandId = 7},
                new Model{Name = "Carisma", BrandId = 7},
                new Model{Name = "Challenger", BrandId = 7},
                new Model{Name = "Colt", BrandId = 7},
                new Model{Name = "Galant", BrandId = 7},
                new Model{Name = "Eclipse", BrandId = 7},
                new Model{Name = "Expo", BrandId = 7},
                new Model{Name = "GTO", BrandId = 7},
                new Model{Name = "Lancer", BrandId = 7},
                new Model{Name = "Outlander", BrandId = 7},
                new Model{Name = "Pajero", BrandId = 7},
                new Model{Name = "Space Star", BrandId = 7},
                new Model{Name = "Space Runner", BrandId = 7},
                new Model{Name = "Sigma", BrandId = 7},
                new Model{Name = "Q50", BrandId = 6},
                new Model{Name = "Q60", BrandId = 6},
                new Model{Name = "Q70", BrandId = 6},
                new Model{Name = "QX30", BrandId = 6},
                new Model{Name = "QX50", BrandId = 6},
                new Model{Name = "QX60", BrandId = 6},
                new Model{Name = "QX80", BrandId = 6},
                new Model{Name = "200", BrandId = 5},
                new Model{Name = "300", BrandId = 5},
                new Model{Name = "300c", BrandId = 5},
                new Model{Name = "Pacifica", BrandId = 5},
                new Model{Name = "Voyager", BrandId = 5},
                new Model{Name = "Conquest", BrandId = 5},
                new Model{Name = "Crossfire", BrandId = 5},
                new Model{Name = "PT Cruiser", BrandId = 5},
                new Model{Name = "Optima", BrandId = 4},
                new Model{Name = "Rio", BrandId = 4},
                new Model{Name = "Forte", BrandId = 4},
                new Model{Name = "Sedona", BrandId = 4},
                new Model{Name = "Sorento", BrandId = 4},
                new Model{Name = "Soul", BrandId = 4},
                new Model{Name = "Stinger", BrandId = 4},
                new Model{Name = "Sportage", BrandId = 4},
                new Model{Name = "Niro", BrandId = 4},
                new Model{Name = "Spectra", BrandId = 4},
                new Model{Name = "Ceed", BrandId = 4},
                new Model{Name = "Roadster", BrandId = 3},
                new Model{Name = "Model S", BrandId = 3},
                new Model{Name = "Model X", BrandId = 3},
                new Model{Name = "Model 3", BrandId = 3},
                new Model{Name = "Range Rover", BrandId = 2},
                new Model{Name = "Range Rover Sport", BrandId = 2},
                new Model{Name = "Range Rover Velar", BrandId = 2},
                new Model{Name = "Range Rover Evoque", BrandId = 2},
                new Model{Name = "Discovery", BrandId = 2},
                new Model{Name = "Defender", BrandId = 2},
                new Model{Name = "718", BrandId = 1},
                new Model{Name = "911", BrandId = 1},
                new Model{Name = "918", BrandId = 1},
                new Model{Name = "944", BrandId = 1},
                new Model{Name = "Taycan", BrandId = 1},
                new Model{Name = "Cayman", BrandId = 1},
                new Model{Name = "Cayenne", BrandId = 1},
                new Model{Name = "Macan", BrandId = 1},
                new Model{Name = "Panamera", BrandId = 1},
                new Model{Name = "Impreza", BrandId = 29},
                new Model{Name = "Legacy", BrandId = 29},
                new Model{Name = "Outback", BrandId = 29},
                new Model{Name = "Ascent", BrandId = 29},
                new Model{Name = "Forester", BrandId = 29},
                new Model{Name = "WRX", BrandId = 29},
                new Model{Name = "BRZ", BrandId = 29},
                new Model{Name = "X-TRAIL", BrandId = 30},
                new Model{Name = "Qashqai", BrandId = 30},
                new Model{Name = "Micra", BrandId = 30},
                new Model{Name = "Juke", BrandId = 30},
                new Model{Name = "GT-R", BrandId = 30},
                new Model{Name = "350Z", BrandId = 30},
                new Model{Name = "370Z", BrandId = 30},
                new Model{Name = "Murano", BrandId = 30},
                new Model{Name = "Pathfinder", BrandId = 30},
                new Model{Name = "Almera", BrandId = 30},
                new Model{Name = "Primera", BrandId = 30},
                new Model{Name = "Berlingo", BrandId = 31},
                new Model{Name = "Jumper", BrandId = 31},
                new Model{Name = "C1", BrandId = 31},
                new Model{Name = "C2", BrandId = 31},
                new Model{Name = "C3", BrandId = 31},
                new Model{Name = "C4", BrandId = 31},
                new Model{Name = "C5", BrandId = 31},
                new Model{Name = "C6", BrandId = 31},
                new Model{Name = "C8", BrandId = 31},
                new Model{Name = "Saxo", BrandId = 31},
                new Model{Name = "Escalade", BrandId = 32},
                new Model{Name = "CT4", BrandId = 32},
                new Model{Name = "CT5", BrandId = 32},
                new Model{Name = "XT4", BrandId = 32},
                new Model{Name = "XT5", BrandId = 32},
                new Model{Name = "XT6", BrandId = 32},
                new Model{Name = "CT", BrandId = 33},
                new Model{Name = "ES", BrandId = 33},
                new Model{Name = "IS", BrandId = 33},
                new Model{Name = "LS", BrandId = 33},
                new Model{Name = "LC", BrandId = 33},
                new Model{Name = "RC", BrandId = 33},
                new Model{Name = "NX", BrandId = 33},
                new Model{Name = "RX", BrandId = 33},
                new Model{Name = "UX", BrandId = 33},
                new Model{Name = "GX", BrandId = 33},
                new Model{Name = "LX", BrandId = 33},
                new Model{Name = "A1", BrandId = 34},
                new Model{Name = "A2", BrandId = 34},
                new Model{Name = "A3", BrandId = 34},
                new Model{Name = "A4", BrandId = 34},
                new Model{Name = "A5", BrandId = 34},
                new Model{Name = "A6", BrandId = 34},
                new Model{Name = "A7", BrandId = 34},
                new Model{Name = "A8", BrandId = 34},
                new Model{Name = "Q2", BrandId = 34},
                new Model{Name = "Q3", BrandId = 34},
                new Model{Name = "Q5", BrandId = 34},
                new Model{Name = "Q7", BrandId = 34},
                new Model{Name = "Q8", BrandId = 34},
                new Model{Name = "TT", BrandId = 34},
                new Model{Name = "E-TRON", BrandId = 34},
                new Model{Name = "80", BrandId = 34},
                new Model{Name = "100", BrandId = 34},
                new Model{Name = "R8", BrandId = 34},
                new Model{Name = "812", BrandId = 35},
                new Model{Name = "458", BrandId = 35},
                new Model{Name = "430", BrandId = 35},
                new Model{Name = "488", BrandId = 35},
                new Model{Name = "Roma", BrandId = 35},
                new Model{Name = "Monza", BrandId = 35},
                new Model{Name = "Enzo", BrandId = 35},
                new Model{Name = "SF90 Stradale", BrandId = 35},
                new Model{Name = "599 GTO", BrandId = 35},
                new Model{Name = "California", BrandId = 35},
                new Model{Name = "Testarossa", BrandId = 35},
                new Model{Name = "LaFerrari", BrandId = 35},
                new Model{Name = "S70", BrandId = 36},
                new Model{Name = "S80", BrandId = 36},
                new Model{Name = "XC40", BrandId = 36},
                new Model{Name = "XC60", BrandId = 36},
                new Model{Name = "XC90", BrandId = 36},
                new Model{Name = "S60/V60", BrandId = 36},
                new Model{Name = "S90/V90", BrandId = 36},
                new Model{Name = "S40/V50", BrandId = 36},
                new Model{Name = "V70/XC70", BrandId = 36},
                new Model{Name = "E-Pace", BrandId = 37},
                new Model{Name = "F-Pace", BrandId = 37},
                new Model{Name = "I-Pace", BrandId = 37},
                new Model{Name = "F-Type", BrandId = 37},
                new Model{Name = "S-type", BrandId = 37},
                new Model{Name = "X-type", BrandId = 37},
                new Model{Name = "XE", BrandId = 37},
                new Model{Name = "XF", BrandId = 37},
                new Model{Name = "XJ", BrandId = 37},
                new Model{Name = "Acadia", BrandId = 38},
                new Model{Name = "Canyon", BrandId = 38},
                new Model{Name = "Savana", BrandId = 38},
                new Model{Name = "Sierra 1500", BrandId = 38},
                new Model{Name = "Sierra 2500", BrandId = 38},
                new Model{Name = "Sierra 3500", BrandId = 38},
                new Model{Name = "Terrain", BrandId = 38},
                new Model{Name = "Yukon", BrandId = 38},
                new Model{Name = "RL", BrandId = 39},
                new Model{Name = "TL", BrandId = 39},
                new Model{Name = "RDX", BrandId = 39},
                new Model{Name = "ILX", BrandId = 39},
                new Model{Name = "MDX", BrandId = 39},
                new Model{Name = "NSX", BrandId = 39},
                new Model{Name = "TLX", BrandId = 39},
                new Model{Name = "TSX", BrandId = 39},
                new Model{Name = "Azure", BrandId = 40},
                new Model{Name = "Bentayga", BrandId = 40},
                new Model{Name = "Arnage", BrandId = 40},
                new Model{Name = "Continental", BrandId = 40},
                new Model{Name = "Continental GT", BrandId = 40},
                new Model{Name = "Continental Flying Spur", BrandId = 40},
                new Model{Name = "Mulsanne", BrandId = 40},
                new Model{Name = "Beetle", BrandId = 41},
                new Model{Name = "Golf", BrandId = 41},
                new Model{Name = "Touran", BrandId = 41},
                new Model{Name = "Tiguan", BrandId = 41},
                new Model{Name = "Touareg", BrandId = 41},
                new Model{Name = "Passat", BrandId = 41},
                new Model{Name = "Phaeton", BrandId = 41},
                new Model{Name = "Jetta", BrandId = 41},
                new Model{Name = "Atlas", BrandId = 41},
                new Model{Name = "Polo", BrandId = 41},
                new Model{Name = "Caddy", BrandId = 41},
                new Model{Name = "Eos", BrandId = 41},
                new Model{Name = "Transporter", BrandId = 41},
                new Model{Name = "Sharan", BrandId = 41},
                new Model{Name = "Scirocco", BrandId = 41},
                new Model{Name = "up!", BrandId = 41},
                new Model{Name = "Amarok", BrandId = 41},
                new Model{Name = "GS", BrandId = 42},
                new Model{Name = "ZS", BrandId = 42},
                new Model{Name = "RX5", BrandId = 42},
                new Model{Name = "HS", BrandId = 42},
                new Model{Name = "Hector", BrandId = 42},
                new Model{Name = "ZS EV", BrandId = 42},
                new Model{Name = "Gloster", BrandId = 42},
                new Model{Name = "V80", BrandId = 42},
                new Model{Name = "7", BrandId = 42},


            });

            data.SaveChanges();
        }
    }
}
