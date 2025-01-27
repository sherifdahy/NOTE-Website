using DAL.Data;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Interface.Controllers
{

    public class AccountController : BaseController
    {

        public AccountController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<ApplicationUser> signInManager) :
            base(context, userManager, roleManager, signInManager)
        {

        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid == true)
            {
                //casting
                ApplicationUser applicationUser = registerVM.Casting();
                // creating
                IdentityResult result = await userManager.CreateAsync(applicationUser, registerVM.Password);
                if (result.Succeeded)
                {
                    // create cookie
                    //await signInManager.SignInAsync(applicationUser, false);

                    await signInManager.SignInWithClaimsAsync(applicationUser, false, new List<Claim>() { 
                        new Claim("Address","41 street elnegma"),
                        new Claim("Type","male")
                    });
                    return RedirectToAction(nameof(Index), "Home");
                }
                else {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(registerVM);
        }
        #endregion


        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid == true) 
            {
                ApplicationUser applicationUser = await userManager.FindByNameAsync(loginVM.Username);
                if (applicationUser != null) {
                    bool found = await userManager.CheckPasswordAsync(applicationUser,loginVM.Password);
                    if (found) {
                        await signInManager.SignInAsync(applicationUser, loginVM.RememberMe);
                        return RedirectToAction("index", "Home");
                    }
                }
                ModelState.AddModelError("", "Username or Password Wrong.");
            
            }
            return View(loginVM);
        }
        #endregion


        #region Singout

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }
        #endregion

    }
}
