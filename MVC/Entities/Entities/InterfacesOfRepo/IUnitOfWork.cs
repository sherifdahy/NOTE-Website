using Entities.Models;
using Entities.Models.Receipt;
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
        UserManager<applicationUser> UserManager { get; }
        RoleManager<IdentityRole<int>> RoleManager { get; }
        IGenericRepo<receipt> Receipts { get; }
        SignInManager<applicationUser> SignInManager { get; }

        void Save();
        
    }
}
