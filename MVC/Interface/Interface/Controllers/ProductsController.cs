using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
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
            string value = Request.Form["search[value]"];
            var products = query.Where(x=> string.IsNullOrEmpty(value)? true : x.InternalId.Contains(value) || x.Name.Contains(value) || x.Description.Contains(value) || x.Code.Contains(value)).OrderByDescending(x=>x.Id).Skip(skip).Take(take).ToList();

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
            return Json(true);

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(new ProductVM().CastingFromModel(IUnitOfWork.Products.GetById(id)));

        }
        [HttpPost]
        public IActionResult Edit(int id ,ProductVM vM)
        {
            IUnitOfWork.Products.Update(vM.CastingToModel(int.Parse(User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value),id));
            IUnitOfWork.Save();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult getDataToDropdownList(string query,int page = 1)
        {
            int pageSize = 10; 
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var hasMore = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { }).Where(x => x.Name.Contains(query)).Count();
            var obj = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { }).Where(x => x.Name.Contains(query)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Json(new
            {
                items = obj,
                more = hasMore
            });


        }
    }
}
