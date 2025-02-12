using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Entities.Models.Receipt;
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
            IGenericRepo<Product> productRepo,
            IGenericRepo<receipt> receiptRepo,
            UserManager<applicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<applicationUser> signInManager) 
        {
            this.context = context;
            Products = productRepo;
            Receipts = receiptRepo;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;

        }
        public IGenericRepo<Product> Products { get; }
        public IGenericRepo<receipt> Receipts { get; }
        public UserManager<applicationUser> UserManager { get; }
        public RoleManager<IdentityRole<int>> RoleManager { get; }
        public SignInManager<applicationUser> SignInManager { get; }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
