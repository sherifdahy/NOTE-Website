using AutoMapper;
using DAL.Data;
using Entities.InterfacesOfRepo;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Interface.Controllers
{
    public class BaseController : Controller
    {
        public IUnitOfWork IUnitOfWork;
        
        public BaseController(IUnitOfWork IUnitOfWork)
        {
            this.IUnitOfWork = IUnitOfWork;
        }
    }
}
