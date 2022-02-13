using HouseSelfInspection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public InspectionController(ApplicationContext context)
        {
            this.context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<InspectionScheduleModel>> Get()
        {
            try
            {
                return context.InspectionSchedules;

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

