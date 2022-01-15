using HouseSelfInspection.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{
    public class LoginController : ControllerBase
    {

        readonly ApplicationContext context;
        public LoginController(ApplicationContext context)
        {
            this.context = context;
        }

        public ActionResult<IEnumerable<LoginModel>> Get()
        {
            string username = "admin";
            string password = "admin";
            LoginModel model = new LoginModel();
            model.username = username;
            model.password = password;
            context.Login.Add(model);
            return context.Login;

        }

        //[HttpPost]
        //[Route("Login")]
        //Post :: /api/login

        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.username);

        //}

    }
}
