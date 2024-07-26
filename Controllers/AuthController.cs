using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Progetto_Settimanale_Vescio_Pia_Francesca.Models;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Interfaces;
using Progetto_Settimanale_Vescio_Pia_Francesca.Services.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Progetto_Settimanale_Vescio_Pia_Francesca.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly IAccountService _accountSvc;

        public AuthController(IAccountService accountService) {
        _accountSvc = accountService;
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult ChoiceSection()
        {

            return View(); 
        }
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult AllUser()
        {
            var users = _accountSvc.GetAllUsers();
            var usersModel = users.Select(u => new AuthModel
            {
                Id = u.Id,
                Username = u.Username,
                Roles = u.Roles,
            }).ToList();
            return View(usersModel); 
        }
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult RemoveUser(int id)
        {
            _accountSvc.RemoveUser(id);
            return RedirectToAction("AllUser", "Auth");
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult AllRoles()
        {
           var roles = _accountSvc.GetAllRoles();
            return View(roles);
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult DetailUser(int id)
        {
            var user = _accountSvc.GetUserById(id);
            var allRoles = _accountSvc.GetAllRoles().Select(role => role.Name).ToList();
            var rolesToRemove = allRoles.Where(r => user.Roles.Contains(r)).ToList();

            foreach(var role in rolesToRemove)
            {
                allRoles.Remove(role);
            }

            ViewBag.Roles = allRoles;
            
            var userModel = new AuthModel { Id = user.Id, Username = user.Username, Roles = user.Roles };
            return View(userModel);
                
        }

        public IActionResult AddRoleToUser(string username, string role)
        {
            _accountSvc.AddUserToRole(username, role);
            return RedirectToAction("AllUser", "Auth");
        }
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult RemoveRoleFromUser(string username, string role)
        {
            _accountSvc.RemoveUserFromRole(username, role);
            return RedirectToAction("AllUser", "Auth");
        }

        public IActionResult CreateRole()
        {
            return View(new RoleModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult CreateRole(RoleModel role)
        {
            _accountSvc.CreateRole(role);
            return RedirectToAction("AllRoles", "Auth");
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult UpdateRole(int id)
        {   
            var r = _accountSvc.Read(id);
            return View(r);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRole(int id, RoleModel role)
        {
            var r = _accountSvc.UpdateModel(id, role);
            return RedirectToAction("AllRoles", "Auth");
        }

        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult DeleteRole(int id)
        {
            _accountSvc.DeleteRole(id);
            return RedirectToAction("AllRoles", "Auth");
        }
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult Register(AuthModel user)
        {
            var u = _accountSvc.Register( new UserDto
            {
                Username = user.Username,
                Password = user.Password,
            });
            return RedirectToAction("Login", "Auth"); 
        }

        [AllowAnonymous]
        [Authorize(Policy = Policies.IsAdmin)]
        public IActionResult FirstPage()
        {
            return View();
        }

        [AllowAnonymous]
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
