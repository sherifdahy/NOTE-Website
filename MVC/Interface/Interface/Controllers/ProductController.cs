using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {

        public ProductController(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            
        }

        public IActionResult Index()
        {
            int id =  Convert.ToInt32( User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            return View(IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == id, new string[] { }));
        }

        public ActionResult Details(int id)
        {
            return View(IUnitOfWork.Products.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            product.ApplicationUserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                IUnitOfWork.Products.Insert(product);
                IUnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(IUnitOfWork.Products.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                IUnitOfWork.Products.UpdateById(product);
                IUnitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(IUnitOfWork.Products.GetById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                IUnitOfWork.Products.Delete(product);
                IUnitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
