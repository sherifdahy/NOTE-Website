using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Interface.Controllers.Receipt
{
    [Authorize]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        [HttpGet]
        public IActionResult Index(int skip = 0)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
            ViewBag.Products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { }, skip, 10).OrderByDescending(x => x.Id);
            ViewBag.count = IUnitOfWork.Products.Count();
            ViewBag.PageCount = (int)Math.Ceiling((double)ViewBag.Count / 10);
            ViewBag.skip = skip;
            return View();
        }


        public IActionResult fuck(string q)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);

            var products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id && x.Name.Contains(q), new string[] { })
                .Select(p => new { id = p.Id, text = p.Name, test = p.Code })
                .ToList();

            return Json(new { results = products });
        }


        [HttpGet("[Controller]/[action]/{text}")]
        public IActionResult SearchProduct(string text)
        {
            #region code
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
            if (text != null)
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
            #endregion

            return PartialView("_tablePartial");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductVM vM)
        {
            if (ModelState.IsValid == true)
            {
                int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
                Product product = vM.Casting(id);
                IUnitOfWork.Products.Insert(product);
                IUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vM);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                IUnitOfWork.Products.Delete(IUnitOfWork.Products.GetById(id));
                IUnitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        public IActionResult DeleteAll()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
            IUnitOfWork.Products.DeleteAll(x => x.ApplicationUserId == id);
            IUnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


        [Route("[Controller]/[action]/{id}")]
        [HttpGet("[controller]/[action]/{id}")]
        // recProduct/edit/1
        public IActionResult Edit(int id)
        {

            return View(IUnitOfWork.Products.GetById(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductVM vM)
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
