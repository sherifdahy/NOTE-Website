using DAL.Data;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class BaseController : Controller
    {
        internal readonly ApplicationDbContext _context;
        internal readonly UserManager<ApplicationUser> userManager;
        internal readonly RoleManager<IdentityRole<int>> roleManager;
        internal readonly SignInManager<ApplicationUser> signInManager;
        public BaseController(ApplicationDbContext applicationDbContext
            ,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole<int>> roleManager
            ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _context = applicationDbContext;
        }
    }
}
