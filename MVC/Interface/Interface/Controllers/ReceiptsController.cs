using Entities.InterfacesOfRepo;
using Interface.ViewModels;
using Interface.ViewModels.ReceiptVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    [Authorize]
    public class ReceiptsController : BaseController
    {
        IWebHostEnvironment env;
        public ReceiptsController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment) : base(unitOfWork)
        {
            env = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult New()
        {

            return View();
        }
        [HttpPost]
        public IActionResult _receiptPartial( [FromBody] ReceiptVM vM) 
        {
            if(ModelState.IsValid)
            {
                
            }
            return PartialView(vM);

        }



        [HttpGet]
        public IActionResult gettaxtype()
        {
            var path = Path.Combine(env.WebRootPath,"json file","TaxType.json");
            var TaxType = System.IO.File.ReadAllText(path);
            
            path = Path.Combine(env.WebRootPath, "json file", "SubTaxType.json");
            var SubTaxType = System.IO.File.ReadAllText(path);

            return Json(new
            {
                taxtype = TaxType,
                subtaxtype = SubTaxType,
            });
        }
    }
}
