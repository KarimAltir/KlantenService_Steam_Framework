using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Models;
using Microsoft.AspNetCore.Identity;
using KlantenService_Steam_Framework.Areas.Identity.Data;

namespace KlantenService_Steam_Framework.Data
{
    public class ApplicationDbContext : IdentityDbContext<KlantenServiceUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public static async Task DataInitializer(ApplicationDbContext context, UserManager<KlantenServiceUser> userManager)
        {
            KlantenServiceUser dummy;
            KlantenServiceUser admin;

            if (!context.Users.Any())
            {
                KlantenServiceUser dummyUser = new KlantenServiceUser
                {
                    Id = "Dummy",
                    Email = "dummy@dummy.xx",
                    UserName = "Dummy",
                    FirstName = "Dummy",
                    LastName = "Dummy",
                    PasswordHash = "Dummy",
                    LockoutEnabled = true,
                    LockoutEnd = DateTime.MaxValue
                };
                context.Users.Add(dummyUser);
                context.SaveChanges();
            

                KlantenServiceUser adminUser = new KlantenServiceUser
                {
                    Id = "Admin",
                    Email = "admin@dummy.xx",
                    UserName = "Admin",
                    FirstName = "Administrator",
                    LastName = "KlantenService"
                };

            var result = await userManager.CreateAsync(adminUser, "Abc!12345");
        }
           

            dummy = context.Users.First(u => u.UserName == "Dummy");
            admin = context.Users.First(u => u.UserName == "Admin");

            AddParameters(context, admin);
            Globals.DummyUser = dummy;  // Make sure the dummy user is always available


            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole { Name = "SystemAdministrator", Id = "SystemAdministrator", NormalizedName = "SYSTEMADMINISTRATOR" },
                    new IdentityRole { Name = "User", Id = "User", NormalizedName = "USER" },
                    new IdentityRole { Name = "UserAdministrator", Id = "UserAdministrator", NormalizedName = "USERADMINISTRATOR" }
                );
                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "SystemAdministrator", UserId = admin.Id });
                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "UserAdministrator", UserId = admin.Id });

                context.SaveChanges();
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        static void AddParameters(ApplicationDbContext context, KlantenServiceUser user)
        {
            if (!context.Parameters.Any())
            {
                context.Parameters.AddRange(
                    new Parameter { Name = "Version", Value = "0.1.0", Description = "Huidige versie van de parameterlijst", Destination = "System", UserId = user.Id },
                    new Parameter { Name = "Mail.Server", Value = "ergens.klantenService.be", Description = "Naam van de gebruikte pop-server", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.Port", Value = "25", Description = "Poort van de smtp-server", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.Account", Value = "SmtpServer", Description = "Acount-naam van de smtp-server", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.Password", Value = "xxxyyy!2315", Description = "Wachtwoord van de smtp-server", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.Security", Value = "true", Description = "Is SSL or TLS encryption used (true or false)", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.SenderEmail", Value = "administrator.klantenService.be", Description = "E-mail van de smtp-verzender", Destination = "Mail", UserId = user.Id },
                    new Parameter { Name = "Mail.SenderName", Value = "Administrator", Description = "Naam van de smtp-verzender", Destination = "Mail", UserId = user.Id }
                );
                context.SaveChanges();
            }

            Globals.Parameters = new Dictionary<string, Parameter>();
            foreach (Parameter parameter in context.Parameters)
            {
                Globals.Parameters[parameter.Name] = parameter;
            }
            //Globals.ConfigureMail();
        }

        public DbSet<KlantenService_Steam_Framework.Models.Game> Games { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.ProblemType> ProblemTypes { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.Complaint> Complaints { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.Parameter> Parameters { get; set; } = default!; 
    }
}
