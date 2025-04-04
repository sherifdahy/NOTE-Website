﻿using AutoMapper;
using Entities.InterfacesOfRepo;
using Interface.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork)
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
