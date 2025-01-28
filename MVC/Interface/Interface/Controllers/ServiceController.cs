using DAL.Data;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    
    public class ServiceController : BaseController
    {

        public ServiceController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<ApplicationUser> signInManager) : base(context, userManager, roleManager, signInManager)
        {
        }
        #region Create Role



        [Authorize(Roles ="Admin")]
        [HttpGet]




        public IActionResult NewRole()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> NewRole(RoleVM vM)
        {
            if (ModelState.IsValid) 
            { 
                IdentityRole<int> role = new IdentityRole<int>() {
                    Name = vM.Name,
                };

                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View(new RoleVM());
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            
            }
            return View(vM);
        }

        #endregion


        
    }
}
