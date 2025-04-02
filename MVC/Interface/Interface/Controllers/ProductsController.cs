using AutoMapper;
using CodeEffects.Rule.Attributes;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Interface.Dto;
using Interface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;

namespace Interface.Controllers
{
    [Authorize]

    public class ProductsController : BaseController
    {
        private readonly int userId;
        private readonly IMapper mapper;
        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor):base(unitOfWork)
        {
            this.mapper = mapper;
            userId = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

        }
        [HttpGet]
        public async Task<IActionResult> Index(int pg = 1)
        {
            IQueryable<Product> products = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == userId)
                .OrderByDescending(X => X.Id);
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int procsCount = products.Count();
            Pager pager = new Pager(procsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            products = products.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = pager;
            if (products != null)
            {
                return View(products);
            }
            return View(new List<Product>());
        }






        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_partialAdd");
        }

        [HttpPost]
        public IActionResult Add(ProductVM vm) 
        {
            if(ModelState.IsValid)
            {
                var temp = mapper.Map<Product>(vm);


                temp.ApplicationUserId =userId;
                IUnitOfWork.Products.Insert(temp);
                IUnitOfWork.Save();
                TempData["success"] = "";
                return Json(new { success = true });
            }
            return PartialView("_partialAdd", vm);
        }

        [HttpGet]
        public IActionResult Delete(int id) {

            IUnitOfWork.Products.Delete(IUnitOfWork.Products.GetById(id));
            IUnitOfWork.Save();
            TempData["success"] = "";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            IUnitOfWork.Products.DeleteAll(x => x.ApplicationUserId == userId);
            IUnitOfWork.Save();
            TempData["success"] = "تم حذف جميع السلع والخدمات بنجاح";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product prod = IUnitOfWork.Products.FindAll(x => x.Id == id).FirstOrDefault();
            ViewBag.id = prod.Id;
            return PartialView("_partialEdit",mapper.Map<ProductVM>(prod));
        }
        
        [HttpPost]
        public IActionResult Edit(int id , ProductVM VM)
        {
            if(ModelState.IsValid)
            {
                var temp = mapper.Map<Product>(VM);
                temp.ApplicationUserId =userId;
                temp.Id = id;
                IUnitOfWork.Products.Update(temp);
                IUnitOfWork.Save();
                TempData["success"] = "";
                return Json(new { success = true});
            }
            ViewBag.id = id;
            return PartialView("_partialEdit",VM);

        }

        [HttpGet]
        public IActionResult getDataToDropdownList(string query,int page = 1)
        {
            int pageSize = 10; 
            var totalCount = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == userId).Where(x => x.Name.Contains(query)).Count();
            var obj = IUnitOfWork.Products.FindAll(x => x.ApplicationUserId == userId)
                .Where(x => x.Name.Contains(query)).Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new { x.InternalId ,  x.CodeType , x.UnitType , x.Code , x.Name,x.UnitPrice , x.Description});

            return Json(new
            {
                items = obj,
                more = (page * pageSize) < totalCount
            });


        }
    }
}
