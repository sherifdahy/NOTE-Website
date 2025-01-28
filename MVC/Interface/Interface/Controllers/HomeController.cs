using Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Interface.Controllers
{
    public class HomeController : Controller
    {
        IWebHostEnvironment hostEnvironment;
        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(IFormFile Image)
        {
            uploadFile.uploadImage(hostEnvironment, Image);
            return View();
        }

    }
}
