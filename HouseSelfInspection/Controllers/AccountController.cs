using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
        readonly UserDbContext _context;
        readonly IConfiguration _configuration;
        readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IHostingEnvironment _env;

        public AccountController(UserManager<ApplicationUserModel> userManager, SignInManager<ApplicationUserModel> signInManager, UserDbContext context, IConfiguration configuration, TokenValidationParameters tokenValidationParameters, IHostingEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _env = env;

        }

        [HttpPost]
        [Route("Invitation", Name = "EmailConfirmRoute")]
        public async Task<IActionResult> EmailConfirm([FromBody] MailRequestModel model)
        {
           
            //model.ToEmail = "mrsakarmaharjan@gmail.com";
            var userExists = await _userManager.FindByEmailAsync(model.ToEmail);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userExists);

            var confirmationLink = Url.Action("InviteUser", "Account", new
            {
                userId = userExists.Id,
                token = token
            }, Request.Scheme);

            //var confirmationLink = $"http://localhost:4200/register?userId={userExists.Id}&token={token}";

            EmailSender emailSender = new EmailSender(_configuration);
            await emailSender.SendEmailAsync(userExists.Email, "Welcome to Self Inspect App", "Please click on the link " + confirmationLink);
            return Ok();

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> InviteUser(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return BadRequest("User not verified");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return BadRequest($"The User ID {userId} is invalid");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest("Not succeeded");
            }
            Redirect("http://localhost:4200");
            return Ok("User Registration Successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Please provide all fields");

                //Using Configuration
                if(credentials.Email == _configuration["Admin:Email"] && credentials.Password == _configuration["Admin:Password"])
                {
                    var userExists = new ApplicationUserModel()
                    {
                        Email = credentials.Email,
                        Name = "Naga",
                        PasswordHash = credentials.Password,
                        Id = _configuration["Admin:UserId"]
                    };
                    

                    var tokenValue = await GenerateTokenAsync(userExists);
                    return Ok(tokenValue);
                }
                else
                {
                    //database
                    var userExists = await _userManager.FindByEmailAsync(credentials.Email);

                    if (userExists != null && userExists.EmailConfirmed && await _userManager.CheckPasswordAsync(userExists, credentials.Password))
                    {
                        var tokenValue = await GenerateTokenAsync(userExists);
                        return Ok(tokenValue);
                    }
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

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM tokenRequestVM)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest("Please provide all fields");

        //        var result = await VerifyAndGenerateTOkenAsync(tokenRequestVM);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //private async Task<AuthResultVM> VerifyAndGenerateTOkenAsync(TokenRequestVM tokenRequestVM)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestVM.RefreshToken);
        //    var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

        //    try
        //    {
        //        var tokenCheckRsult = jwtTokenHandler.ValidateToken(tokenRequestVM.Token, _tokenValidationParameters, out var validatedToken);

        //    }
        //    catch (SecurityTokenExpiredException)
        //    {

        //        if(storedToken.DateExpire >= DateTime.UtcNow)
        //        {
        //            await GenerateTokenAsync(dbUser);
        //        }
        //    }
        //}

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

                //var token = await _userManager.GeneratePasswordResetTokenAsync(userExists);

                //var reset = await _userManager.ResetPasswordAsync(userExists, token, registerVM.Password);

                return Ok(_userManager.UpdateAsync(userExists));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPut("upload/{tenantId}"), DisableRequestSizeLimit]
        public async Task<string> Put(string tenantId)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(_env.WebRootPath, "tenants", "Images");
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var filePath = string.Empty;
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    filePath = "http://" + HttpContext.Request.Host.Value + "/tenants/Images/" + fileName;

                    ApplicationUserModel userExists = await _userManager.FindByIdAsync(tenantId);
                    userExists.ImagePath = filePath;

                    await _userManager.UpdateAsync(userExists);
                    return filePath;

                }
                else
                {
                    return "Not successful";
                }


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

                //validate token and userid

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
                    //EmailConfirmed = true,
                    RoleId = 3,

                    
                };

                var result = await _userManager.CreateAsync(newUser, registerVM.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _signInManager.SignInAsync(newUser, isPersistent: false);

                return Ok(GenerateTokenAsync(newUser));

            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }

        private async Task<AuthResultVM> GenerateTokenAsync(ApplicationUserModel user)
        {
            var authClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken
             (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
             );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = new RefreshTokenModel()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo,
                UserDetail = user
                UserId = user.Id
            };

            var result = response;

            return result;
        }




        [HttpGet("get-all")]
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
                    HouseId = item.HouseId,
                    ImagePath = item.ImagePath
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
