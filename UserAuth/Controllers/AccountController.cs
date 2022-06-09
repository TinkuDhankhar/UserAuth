using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuth.Data;
using UserAuth.Models;

namespace UserAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser login)
        {
            var user = await _userManager.FindByEmailAsync(login.UserName);
            if (user != null)
            {
                var LoginUser = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (LoginUser.Succeeded)
                {
                    //UserRole.GetUserRole = await GetRoles(user);
                    //if (User.IsInRole("admin"))
                    //{
                        return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{
                    //    return View();
                    //}
                    //else
                    //{
                    //    return RedirectToAction(nameof());
                    //}
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User name and password");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User dosn't exits");
                return View(login);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = register.UserName,
                    UserName = register.UserName
                };
                var userRegister = await _userManager.CreateAsync(user, register.ConfirmPassword);
                if (userRegister.Succeeded)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var item in userRegister.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View(register);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invaid format.");
                return View(register);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<bool> IsExit(string UserName)
        {
            if (await _userManager.Users.Where(x => x.UserName.Equals(UserName)).AnyAsync())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [HttpGet]
        public async Task<string[]> GetRoles(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToArray();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
