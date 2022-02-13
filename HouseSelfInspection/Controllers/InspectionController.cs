using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseSelfInspection.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InspectionController : Controller
    {

        private readonly ApplicationContext context;
        readonly UserDbContext _context;
        private readonly IHostingEnvironment env;
        readonly UserManager<ApplicationUserModel> _userManager;
        readonly SignInManager<ApplicationUserModel> _signInManager;
        readonly IConfiguration _configuration;
        readonly TokenValidationParameters _tokenValidationParameters;

        public InspectionController(ApplicationContext context, UserManager<ApplicationUserModel> userManager)
        {
            this.context = context;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public async Task<List<InspectionViewModel>> Get()
        {
            try
            {
                var InspectionList = context.InspectionSchedules.ToList();
                HouseController houseController = new HouseController(context, env, _userManager);
                AccountController accountController = new AccountController(_userManager, _signInManager, _context, _configuration, _tokenValidationParameters, env);

                List<InspectionViewModel> insepctionVMList = new List<InspectionViewModel>();

                foreach (var item in InspectionList)
                {
                    InspectionViewModel model = new InspectionViewModel();

                    model.InspectionScheduleId = item.InspectionScheduleId;

                    model.HouseId = item.HouseId;
                    var house = houseController.GetById(item.HouseId);
                    model.HouseAddress = house.House_number + " " + house.Street + ", " + house.Suburb + ", " + house.State + house.Postal_code;

                    model.UserId = item.UserId;

                    ApplicationUserModel userExists = await _userManager.FindByIdAsync(model.UserId);
                    model.UserName = userExists.UserName;

                    model.Inspection_date = item.Inspection_date;

                    model.Inspection_status = item.Inspection_status;

                    insepctionVMList.Add(model);

                }

                return insepctionVMList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{id}")]
        public InspectionScheduleModel GetById(int id)
        {
            try
            {
                return context.InspectionSchedules.Find(id);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("GetByUserId")]
        public IQueryable<InspectionScheduleModel> GetByUserId([FromBody] InspectionScheduleModel model)
        {
            try
            {

                return context.InspectionSchedules.Where(x => x.UserId == model.UserId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("ViewHouseByInspectionId")]
        public HouseModel ViewHouseByInspectionId([FromBody] InspectionScheduleModel model)
        {
            InspectionScheduleModel inspection = context.InspectionSchedules.Find(model.InspectionScheduleId);
            HouseModel house = context.Houses.Find(inspection.HouseId);
            return house;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InspectionScheduleModel model)
        {
            try
            {
                context.InspectionSchedules.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] InspectionScheduleModel model)
        {
            try
            {
                if(id != model.InspectionScheduleId)
                {
                    return BadRequest();
                }
                else
                {
                    context.Entry(model).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(model);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                context.InspectionSchedules.Remove(context.InspectionSchedules.Find(id));
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("PostFeedback")]
        public async Task<IActionResult> PostFeedback([FromBody] FeedbackModel model)
        {
            try
            {
                context.Feedbacks.Add(model);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





    }
    


}

