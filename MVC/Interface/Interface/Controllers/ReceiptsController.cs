using Entities.InterfacesOfRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        public ReceiptsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {

            return View();
        }
    }
}
