using Entities.InterfacesOfRepo;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Authorize]

    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult getData()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var query = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { });
            int recordsTotals = query.Count();
            int skip = int.Parse(Request.Form["start"]);
            int take = int.Parse(Request.Form["length"]);
            var products = query.Skip(skip).Take(take).ToList();

            return Json(new
            {
                recordsFiltered = recordsTotals,
                recordsTotals,
                data = products
            });
        }

        [HttpPost]
        public IActionResult Add(ProductVM vM) 
        {
            if(ModelState.IsValid)
            {
                IUnitOfWork.Products.Insert(vM.CastingToModel(
                    int.Parse(User.Claims.FirstOrDefault(x => x.Type
                    == ClaimTypes.NameIdentifier).Value)
                    ));
                IUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id) {

            IUnitOfWork.Products.Delete(IUnitOfWork.Products.GetById(id));
            IUnitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            return RedirectToAction("Index");

        }
    }
}
