using AutoMapper;
using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.Dto;
using Interface.Services.ApiCall;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Interface.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IApiCall apiCall;
        private readonly IMapper mapper;

        public object? Jsonconvert { get; private set; }

        public AccountController(IUnitOfWork unitOfWork,IApiCall apiCall,IMapper mapper) :base(unitOfWork) 
        {
            this.apiCall = apiCall;
            this.mapper = mapper;
        }


        #region Register
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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


        #region TaxpyerProfile
        [HttpGet]
        public async Task<IActionResult> TaxpayerProfile()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ApplicationUser applicationUser = await IUnitOfWork.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == id );
            var temp = mapper.Map<TaxpayerProfileVM>(applicationUser);
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxpayerProfile(TaxpayerProfileVM vM)
        {
            if (ModelState.IsValid) {
                ApplicationUser applicationUser = await IUnitOfWork.UserManager.FindByIdAsync(User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value);
                mapper.Map(vM, applicationUser);
                IdentityResult result = await IUnitOfWork.UserManager.UpdateAsync(applicationUser);
                if (!result.Succeeded) {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View(vM);
                    }
                }
                TempData["Message"] = JsonConvert.SerializeObject(new MessageVM()
                {
                    Icon = "success",
                    Title = "نجاح العملية",
                    Message = "تم تحديث البيانات بنجاح."
                });
                return RedirectToAction(nameof(TaxpayerProfile));
            }
            return View(vM);
        }
        #endregion

    }
}
