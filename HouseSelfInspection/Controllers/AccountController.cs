using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{

    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {

        readonly UserManager<ApplicationUserModel> _userManager;
        readonly SignInManager<ApplicationUserModel> _signInManager;

        //readonly RoleManager<ApplicationUserModel> _roleManager;
        readonly ApplicationContext _context;
        readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager, ApplicationContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _context = context;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Please provide all fields");

                //Using Usermanager
                var userExists = await _userManager.FindByEmailAsync(credentials.Email);

                //Delete this later
                userExists.EmailConfirmed = true;

                if(userExists != null && userExists.EmailConfirmed && await _userManager.CheckPasswordAsync(userExists, credentials.Password))
                {
                    return Ok(CreateToken(userExists));
                }
                return Unauthorized();

                //Using Signinmanager
                //var result = await _signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, false);

                //if (!result.Succeeded)
                //    return BadRequest();

                //var user = await _userManager.FindByEmailAsync(credentials.Username);

                //return Ok(CreateToken(user));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId, [FromBody] RegisterUserViewModel registerVM)
        {
            try
            {
                if (!userId.Equals(registerVM.UserId))
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                    return BadRequest("Please, provide all the required fields");

                ApplicationUserModel userExists = await _userManager.FindByIdAsync(registerVM.UserId);

                if (userExists == null)
                    return BadRequest("The user doesnot exist");

                userExists.Name = registerVM.Name;
                userExists.Email = registerVM.Email;
                userExists.PhoneNumber = registerVM.Contact;
                userExists.StartDate = registerVM.StartDate;
                userExists.UserName = registerVM.Email;
                userExists.HouseId = registerVM.HouseId;

                var token = await _userManager.GeneratePasswordResetTokenAsync(userExists);

                var reset = await _userManager.ResetPasswordAsync(userExists, token, registerVM.Password);

                return Ok(_userManager.UpdateAsync(userExists));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel registerVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Please, provide all the required fields");

                var userExists = await _userManager.FindByEmailAsync(registerVM.Email);

                if (userExists != null)
                    return BadRequest($"User {registerVM.Email} already exists");


                ApplicationUserModel newUser = new ApplicationUserModel
                {
                    Name = registerVM.Name,
                    PhoneNumber = registerVM.Contact,
                    Email = registerVM.Email,
                    UserName = registerVM.Email,
                    StartDate = registerVM.StartDate,
                    HouseId = registerVM.HouseId,
                    SecurityStamp = Guid.NewGuid().ToString(),

                    //Delete later
                    EmailConfirmed = true,
                    RoleId = 3,

                    
                };

                var result = await _userManager.CreateAsync(newUser, registerVM.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return Ok(CreateToken(newUser));

            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }

        string CreateToken(ApplicationUserModel user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var sigingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));

            var signingCredentials = new SigningCredentials(sigingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        

        [HttpGet]
        public ActionResult<IEnumerable<RegisterUserViewModel>> GetUsers()
        {
            try
            {
                List<RegisterUserViewModel> userlist = new List<RegisterUserViewModel>();

                foreach (var item in _userManager.Users.ToList())
                {
                    userlist.Add(
                        new RegisterUserViewModel()
                        {
                            UserId = item.Id,
                            Name = item.Name,
                            Contact = item.PhoneNumber,
                            Email = item.Email,
                            StartDate = item.StartDate,
                            Username = item.UserName,
                            HouseId = item.HouseId
                        }
                    );
                }

                return userlist;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterUserViewModel>> GetByIdAsync(String id)
        {
            try
            {
                ApplicationUserModel item = await _userManager.FindByIdAsync(id);
                return new RegisterUserViewModel()
                {
                    Name = item.Name,
                    Contact = item.PhoneNumber,
                    Email = item.Email,
                    StartDate = item.StartDate,
                    Username = item.UserName,
                    HouseId = item.HouseId
                };


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ApplicationUserModel item = await _userManager.FindByIdAsync(id);
                return Ok(await _userManager.DeleteAsync(item));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
