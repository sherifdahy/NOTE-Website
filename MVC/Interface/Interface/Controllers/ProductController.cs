using DAL.Data;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return View(context.Products.ToList());
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return View(context.Products.Find(id));
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return View(context.Products.Find(id));
            }
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Products.Update(product);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return View(context.Products.Find(id));
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Products.Remove(product);
                    context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
