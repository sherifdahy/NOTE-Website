using Entities.InterfacesOfRepo;
using Interface.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers.Receipt
{
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
        public IActionResult New(ReceiptVM vM)
        {
                return View();
        }


        [HttpGet]
        public IActionResult GetTaxType()
        {
            var path = Path.Combine(env.WebRootPath, "json file", "TaxType.json");
            var taxtype = System.IO.File.ReadAllText(path);

            path = Path.Combine(env.WebRootPath, "json file", "SubTaxType.json");
            var subtaxtype = System.IO.File.ReadAllText(path);


            var obj = new
            {
                taxtype = taxtype,
                subtaxtype = subtaxtype,
            };
            return Json(obj);
        }

        
    }
}
