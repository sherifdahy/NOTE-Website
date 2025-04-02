using Interface.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class ErrorController : Controller
    {
        
        [HttpGet]
        public IActionResult Index(List<ErrorResponseDTO> errors)
        {
            return View(errors);
        }
    }
}
