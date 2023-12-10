using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KlantenService_Steam_Framework.Models;

namespace KlantenService_Steam_Framework.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KlantenService_Steam_Framework.Models.Game> Game { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.ProblemType> ProblemType { get; set; } = default!;
        public DbSet<KlantenService_Steam_Framework.Models.Complaint> Complaint { get; set; } = default!;
    }
}
