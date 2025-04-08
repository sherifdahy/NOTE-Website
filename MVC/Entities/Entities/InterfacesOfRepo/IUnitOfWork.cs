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

namespace Entities.InterfacesOfRepo
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Receipt> Receipts { get; }
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<IdentityRole<int>> RoleManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }

        void Save();
    }
}
