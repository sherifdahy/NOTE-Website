using Entities.InterfacesOfRepo;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers.Receipt
{
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
