using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{

    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {

        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            try
            {
                var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };
                var result = await userManager.CreateAsync(user, credentials.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await signInManager.SignInAsync(user, isPersistent: false);

                var jwt = new JwtSecurityToken();
                return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }
    }
}
