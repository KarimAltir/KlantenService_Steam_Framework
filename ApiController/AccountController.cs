using KlantenService_Steam_Framework.ApiModel;
using KlantenService_Steam_Framework.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlantenService_Steam_Framework.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SignInManager<KlantenServiceUser> _signInManager;

        public AccountController(SignInManager<KlantenServiceUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet("{name}/{password}")]
        public async Task<ActionResult<Boolean>> LoginGet(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false) ;

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Boolean>> PutAccount(LoginModel @login)
        {
            var result = await _signInManager.PasswordSignInAsync(@login.UserName, @login.Password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
