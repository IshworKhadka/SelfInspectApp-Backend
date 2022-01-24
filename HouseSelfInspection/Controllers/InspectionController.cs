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

        [Authorize]
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




        private InspectionScheduleModel[] inspectionList = new InspectionScheduleModel[]

        {
            new InspectionScheduleModel
            {
                InspectionScheduleId = 1, TenantId = 1, Inspection_date = new DateTime(), Inspection_status = "", HouseId = 1
            },
            new InspectionScheduleModel
            {
                InspectionScheduleId = 2, TenantId = 1, Inspection_date = new DateTime(), Inspection_status = "", HouseId = 3
            },
            new InspectionScheduleModel
            {
                InspectionScheduleId = 3, TenantId = 2, Inspection_date = new DateTime(), Inspection_status = "", HouseId = 3
            },
            new InspectionScheduleModel
            {
                InspectionScheduleId = 4, TenantId = 2, Inspection_date = new DateTime(), Inspection_status = "", HouseId = 4,
            },
            new InspectionScheduleModel
            {
                InspectionScheduleId = 5, TenantId = 2, Inspection_date = new DateTime(), Inspection_status = "", HouseId = 5
            }

         };

    }
    


}

