using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountSvc;

        public AuthController(IAccountService accountService) {
        _accountSvc = accountService;
        }

        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AuthModel user)
        {
            var u = _accountSvc.Register( new UserDto
            {
                Username = user.Username,
                Password = user.Password,
            });
            return RedirectToAction("FirstPage", "Auth"); 
        }

        public IActionResult FirstPage()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthModel user)
        { 
            {
                var u = _accountSvc.Login(user.Password, user.Username);
                if (u != null)
                {
                var claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Name, u.Username)
                    };
                    u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                }
               
            }
            return RedirectToAction("Index", "Home");

         
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    
    }
}
