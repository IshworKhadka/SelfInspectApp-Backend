using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSelfInspection.Models;
using HouseSelfInspection.Models.Static_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseSelfInspection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        private readonly ApplicationContext context;
        readonly UserManager<ApplicationUserModel> _userManager;

        public ValuesController(ApplicationContext context, UserManager<ApplicationUserModel> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<HouseSectionModel>> Get()
        {
            try
            {
                return context.HouseSections;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet("{id}")]
        [Route("GetNameById")]
        public async Task<string> GetNameById(string id)
        {
            try
            {
                ApplicationUserModel item = await _userManager.FindByIdAsync(id);
                return item.UserName;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
