using KlantenService_Steam_Framework.Areas.Identity.Data;
using KlantenService_Steam_Framework.Data;
using KlantenService_Steam_Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KlantenService_Steam_Framework.Controllers
{
    [Authorize(Roles = "UserAdministrator")]
    public class UsersController : Controller
    {
        ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(string userName, string firstName, string lastName, string email)
        {
            List<UserIndexViewModel> vmUsers = new List<UserIndexViewModel>();
            List<KlantenServiceUser> users = _context.Users
                                            .Where(u => u.UserName != "Dummy"
                                                    && (u.UserName.Contains(userName) || string.IsNullOrEmpty(userName))
                                                    && (u.FirstName.Contains(firstName) || string.IsNullOrEmpty(firstName))
                                                    && (u.LastName.Contains(lastName) || string.IsNullOrEmpty(lastName))
                                                    && (u.Email.Contains(email) || string.IsNullOrEmpty(email))
                                                   )
                                            .ToList();

            foreach (KlantenServiceUser user in users)
            {
                vmUsers.Add(new UserIndexViewModel
                {
                    Blocked = !user.LockoutEnabled,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Roles = (from userRole in _context.UserRoles
                             where userRole.UserId == user.Id
                             orderby userRole.RoleId
                             select userRole.RoleId).ToList()
                });
            }
            ViewData["userName"] = userName;
            ViewData["firstName"] = firstName;
            ViewData["lastName"] = lastName;
            ViewData["email"] = email;
            return View(vmUsers);
        }

        public IActionResult UnBlock(string userName)
        {
            KlantenServiceUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            user.LockoutEnabled = true;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Block(string userName)
        {
            KlantenServiceUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            user.LockoutEnabled = false;
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Roles(string userName)
        {
            KlantenServiceUser user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            UserRolesViewModel roleViewModel = new UserRolesViewModel
            {
                UserName = userName,
                Roles = (from userRole in _context.UserRoles
                         where userRole.UserId == user.Id
                         orderby userRole.RoleId
                         select userRole.RoleId).ToList()
            };
            ViewData["AllRoles"] = new MultiSelectList(_context.Roles.OrderBy(r => r.Name), "Id", "Name", roleViewModel.Roles);
            return View(roleViewModel);
        }

        [HttpPost]
        public IActionResult Roles([Bind("UserName, Roles")] UserRolesViewModel _model)
        {
            KlantenServiceUser user = _context.Users.FirstOrDefault(u => u.UserName == _model.UserName);

            // Bestaande rollen ophalen
            List<IdentityUserRole<string>> roles = _context.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
            foreach (IdentityUserRole<string> role in roles)
                _context.Remove(role);

            // Nieuwe rollen toekennen
            if (_model.Roles != null)
                foreach (string roleId in _model.Roles)
                    _context.UserRoles.Add(new IdentityUserRole<string> { RoleId = roleId, UserId = user.Id });

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
