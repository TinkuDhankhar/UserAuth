using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UserAuth.Models;

namespace UserAuth.Controllers
{
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this._signInManager = signInManager; this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.Select(s => new UserRegister()
            {
                Id = s.Id,
                UserName = s.UserName
            }).ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return View(await _userManager.Users.Select(s => new UserRegister()
            {
                Id = s.Id,
                UserName = s.UserName
            }).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _userManager.Users.Where(x => x.Id.Equals(id)).Select(s => new UserRegister()
            {
                Id = s.Id,
                UserName = s.UserName
            }).FirstOrDefaultAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRegister user)
        {

            var editUser = await _userManager.FindByIdAsync(user.Id);
            editUser.Email = user.UserName;
            editUser.UserName = user.UserName;
            if (editUser != null)
            {
                var userEdit = await _userManager.UpdateAsync(editUser);
                if (userEdit.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Some technical problem");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Some Technical problem");
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var deleteUser = await _userManager.FindByIdAsync(id);
            if (deleteUser != null)
            {
                await _userManager.DeleteAsync(deleteUser);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        
    }
}