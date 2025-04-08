using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Document;
using Entities.Models.Document.BaseComponent;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context,
            IRepository<Product> productRepo,
            IRepository<Receipt> receiptRepo,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<ApplicationUser> signInManager) 
        {
            this.context = context;
            Products = productRepo;
            Receipts = receiptRepo;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;

        }
        public IRepository<Product> Products { get; }
        public IRepository<Receipt> Receipts { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole<int>> RoleManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
