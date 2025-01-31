using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Authorize]
    public class recProductController : BaseController
    {
        public recProductController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            
        }
        
        [HttpGet]
        public IActionResult Index(int skip = 0)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
            ViewBag.Products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { },skip,10).OrderByDescending(x=>x.Id);
            ViewBag.count = IUnitOfWork.Products.Count();
            ViewBag.PageCount = (int)Math.Ceiling((double)ViewBag.Count / 10);
            ViewBag.skip = skip;
            return View();
        }

        [HttpGet]
        public IActionResult SearchProduct(string text)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);



            if(text != null)
            {
                ViewBag.Products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id && x.Name.Contains(text), new string[] { }, 0, 10).OrderByDescending(x => x.Id);
            }
            else
            {
                ViewBag.Products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { }, 0, 10).OrderByDescending(x => x.Id);
            }



            ViewBag.count = IUnitOfWork.Products.Count();
            ViewBag.PageCount = (int)Math.Ceiling((double)ViewBag.Count / 10);
            ViewBag.skip = 0;
                
            return PartialView("_tablePartial");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductVM vM) 
        {
            if (ModelState.IsValid == true) {
                int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
                Product product = vM.Casting(id);
                IUnitOfWork.Products.Insert(product);
                IUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index),vM);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id !=0) {
                IUnitOfWork.Products.Delete(IUnitOfWork.Products.GetById(id));
                IUnitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        public IActionResult DeleteAll ()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
            IUnitOfWork.Products.DeleteAll(x=>x.ApplicationUserId == id);
            IUnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            return View(IUnitOfWork.Products.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,ProductVM vM)
        {
            if (ModelState.IsValid == true)
            {
                Product product = vM.Casting(int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));
                product.Id = id;
                IUnitOfWork.Products.UpdateById(product);
                IUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vM);

        }
    }
}
