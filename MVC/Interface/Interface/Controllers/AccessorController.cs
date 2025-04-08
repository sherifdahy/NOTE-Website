using Entities.InterfacesOfRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class AccessorController : BaseController
    {
        private readonly IWebHostEnvironment env;
        public AccessorController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment):base(unitOfWork)
        {
            env = webHostEnvironment;
        }

        #region GetTaxType
        [HttpGet]
        public IActionResult GetTaxType()
        {
            string _folder = "JsonFiles";
            var path = Path.Combine(env.WebRootPath, _folder, "TaxType.json");
            var TaxType = System.IO.File.ReadAllText(path);
            path = Path.Combine(env.WebRootPath, _folder, "SubTaxType.json");
            var SubTaxType = System.IO.File.ReadAllText(path);

            return Json(new
            {
                taxtype = TaxType,
                subtaxtype = SubTaxType,
            });
        }
        #endregion
    }
}
