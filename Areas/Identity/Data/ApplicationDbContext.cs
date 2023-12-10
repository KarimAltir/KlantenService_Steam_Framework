using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Models;
using Microsoft.AspNetCore.Identity;

namespace KlantenService_Steam_Framework.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<KlantenServiceUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public static async Task DataInitializer(ApplicationDbContext context, UserManager<KlantenServiceUser> userManager)
        {
            AddParameters();
            
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
                    LastName = "GroupSpace"
                };

                var result = await userManager.CreateAsync(adminUser, "Abc!12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        static void AddParameters()
        {

        }

        public DbSet<KlantenService_Steam_Framework.Models.Game> Game { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.ProblemType> ProblemType { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.Complaint> Complaint { get; set; } = default!;
    }
}
