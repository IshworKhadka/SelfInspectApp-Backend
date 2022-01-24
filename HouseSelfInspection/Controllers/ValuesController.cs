using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSelfInspection.Models.Static_Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseSelfInspection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {

        private readonly ApplicationContext context;

        public ValuesController(ApplicationContext context)
        {
            this.context = context;
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
