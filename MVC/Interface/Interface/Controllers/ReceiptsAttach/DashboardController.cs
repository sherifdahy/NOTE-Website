using Entities.InterfacesOfRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers.Receipt
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
