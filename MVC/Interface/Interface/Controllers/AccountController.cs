using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Interface.Controllers
{

    public class AccountController : BaseController
    {

        public AccountController(IUnitOfWork unitOfWork):base(unitOfWork) 
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
                IdentityResult result = await IUnitOfWork.UserManager.CreateAsync(applicationUser, registerVM.Password);
                if (result.Succeeded)
                {

                    //Assign Role

                    #region Assign Role to User
                    IdentityResult nested_result = await IUnitOfWork.UserManager.AddToRoleAsync(applicationUser, "User");
                    if (nested_result.Succeeded)
                    {
                        // create cookie

                        await IUnitOfWork.SignInManager.SignInWithClaimsAsync(applicationUser, false, new List<Claim>() {
                        
                        });
                        return RedirectToAction(nameof(Index), "Home");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                    #endregion
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
                ApplicationUser applicationUser = await IUnitOfWork.UserManager.FindByEmailAsync(loginVM.Email);
                if (applicationUser != null) 
                {
                    bool found = await IUnitOfWork.UserManager.CheckPasswordAsync(applicationUser,loginVM.Password);
                    if (found) {
                        await IUnitOfWork.SignInManager.SignInWithClaimsAsync(applicationUser, loginVM.RememberMe, new List<Claim>() {
                        
                        });
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
            await IUnitOfWork.SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }
        #endregion

    }
}
