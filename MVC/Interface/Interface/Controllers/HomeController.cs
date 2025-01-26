using Interface.Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Interface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Emp employee= new Emp() { 
                id = 1,name = "sherif",age = 17 , description = "Software Engineer"
            };
            EmpAndDeptVM vm = new EmpAndDeptVM()
            {
                emp = employee,
                Dept = "Communication and Computer"
            };
            return View(vm);
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
    }
}
