using AutoMapper;
using Entities.InterfacesOfRepo;
using Interface.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Claims;

namespace Interface.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly int _userId;
        public HomeController(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork)
        {
            _userId = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value); 
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReceiptsDashboard(DateTime startDate , DateTime endDate)
        {
            var receipts = await IUnitOfWork.Receipts
                .FindAllAsync(x =>  x.ApplicationUserId == _userId && x.Header.DateTimeIssued >= startDate && x.Header.DateTimeIssued <= endDate);
            return PartialView(receipts);
        }
        [HttpGet]
        public IActionResult InvoicesDashboard(DateTime startDate, DateTime endDate)
        {
            return PartialView();
        }

    }
}
