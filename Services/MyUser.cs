using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Data;
using Microsoft.IdentityModel.Tokens;

namespace KlantenService_Steam_Framework.Services
{
    public interface IMyUser
    {
        public KlantenServiceUser User();
    }
    public class MyUser : IMyUser
    {
        readonly ApplicationDbContext _context;
        readonly IHttpContextAccessor _httpContext;
        KlantenServiceUser user = null;

        public MyUser(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public KlantenServiceUser User()
        {
            string name = _httpContext.HttpContext.User.Identity.Name;
            if (user == null || user.UserName != name)
                user = _context.Users.First(u => u.UserName == (string.IsNullOrEmpty(name) ? "Dummy" : name));

            return user;
        }
    }
}
