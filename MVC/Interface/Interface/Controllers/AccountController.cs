using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Interface.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {

        public AccountController(IUnitOfWork unitOfWork):base(unitOfWork) 
        {
            
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
                applicationUser applicationUser = registerVM.Casting();
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
                applicationUser applicationUser = await IUnitOfWork.UserManager.FindByEmailAsync(loginVM.Email);
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
            applicationUser applicationUser = await IUnitOfWork.UserManager.Users.FirstOrDefaultAsync(x=>x.Id == id );
            return View(CastingToViewModel(applicationUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxpayerProfile(TaxpyerProfileVM vM)
        {
            if (ModelState.IsValid) {
                applicationUser applicationUser = await IUnitOfWork.UserManager.FindByIdAsync(User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value);
                IdentityResult result = await IUnitOfWork.UserManager.UpdateAsync(CastingToModel(vM,applicationUser));
                if (!result.Succeeded) {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vM);
        }
        #endregion


        #region Casting
        public TaxpyerProfileVM CastingToViewModel(applicationUser applicationUser)
        {
            return new TaxpyerProfileVM()
            {
                Name = applicationUser.Name,
                RegistrationNumber = applicationUser.RegistrationNumber,
                Email = applicationUser.Email,
                Mobile = applicationUser.Mobile,
                POSSerial = applicationUser.POSSerial,
                POSClientId = applicationUser.POSClientId,
                POSClientSecret = applicationUser.POSClientSecret,
                ActivityCodes = applicationUser.ActivityCodes,
                BranchCode = applicationUser.BranchCode,
                BranchAddress = new BranchAddressVM()
                {
                    BuildingNumber = applicationUser.BuildingNumber,
                    Country = applicationUser.Country,
                    Governate = applicationUser.Governate,
                    RegionCity = applicationUser.RegionCity,
                    Street = applicationUser.Street,
                }
            };
        }

        public applicationUser CastingToModel(TaxpyerProfileVM vM,applicationUser applicationUser)
        {
            applicationUser.Name = vM.Name;
            applicationUser.RegistrationNumber = vM.RegistrationNumber;
            applicationUser.Country = vM.BranchAddress.Country;
            applicationUser.Governate = vM.BranchAddress.Governate;
            applicationUser.RegionCity = vM.BranchAddress.RegionCity;
            applicationUser.Street = vM.BranchAddress.Street;
            applicationUser.ActivityCodes = vM.ActivityCodes;
            applicationUser.BuildingNumber = vM.BranchAddress.BuildingNumber;
            applicationUser.Mobile = vM.Mobile;
            applicationUser.POSSerial = vM.POSSerial;
            applicationUser.POSClientId = vM.POSClientId;
            applicationUser.POSClientSecret = vM.POSClientSecret;
            applicationUser.BranchCode = vM.BranchCode;
            return applicationUser;
        }
        #endregion
    }
}
