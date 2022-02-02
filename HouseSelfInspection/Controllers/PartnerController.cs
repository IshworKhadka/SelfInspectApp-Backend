using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{
    public class PartnerController : Controller
    {

        //private readonly IWebHostEnvironment _env;
        //private readonly IPartner _repo;

        //public PartnerController(IPartner repo, IWebHostEnvironment env)
        //{
        //    _env = env;
        //    _repo = repo;

        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
