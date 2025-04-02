using Entities.Models;
using Entities.Models.Document.BaseComponent;
using Entities.Models.Document.Receipt;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.InterfacesOfRepo
{
    public interface IUnitOfWork
    {
        IGenericRepo<Product> Products { get; }
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole<int>> RoleManager { get; }
        IGenericRepo<Receipt> Receipts { get; }
        SignInManager<ApplicationUser> SignInManager { get; }

        void Save();
        
    }
}
