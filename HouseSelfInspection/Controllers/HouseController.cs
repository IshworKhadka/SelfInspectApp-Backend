using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using HouseSelfInspection.Models;
using HouseSelfInspection.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseSelfInspection.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class HouseController : Controller
    {

        private readonly IHostingEnvironment env;
        private readonly ApplicationContext context;
        readonly UserManager<ApplicationUserModel> _userManager;

        public HouseController(ApplicationContext context,
            IHostingEnvironment env, UserManager<ApplicationUserModel> userManager)
        {
            this.context = context;
            this.env = env;
            this._userManager = userManager;
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<HouseModel>> GetAll()
        {
            try
            {
                return context.Houses;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{id}")]
        public HouseModel GetById(int id)
        {
            try
            {
                return context.Houses.Find(id);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("houseByUserId/{userId}")]
        public async Task<HouseModel> Get(string userId)
        {
            try
            {
                ApplicationUserModel user = await _userManager.FindByIdAsync(userId);
                return context.Houses.Find(user.HouseId);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HouseModel model)
        {
            try
            {
                var userId = HttpContext.User.Claims.First().Value;
                context.Houses.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("{houseId}")]
        public async Task<IActionResult> Put(int houseId, [FromBody] HouseModel model)
        {
            try
            {
                if (houseId != model.HouseId)
                {
                    return BadRequest();
                }
                
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        [HttpPut("upload/{houseId}"), DisableRequestSizeLimit]
        public async Task<string> Put(int houseId)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(env.WebRootPath, "house", "Images");
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
                    filePath = "http://" + HttpContext.Request.Host.Value + "/house/Images/" + fileName;

                    HouseModel model = await context.Houses.FindAsync(houseId);
                    model.ImgPath = filePath;
                    context.Houses.Update(model);
                    await context.SaveChangesAsync();
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                context.Houses.Remove(context.Houses.Find(id));
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm] ImageModel imgModel)
        {
            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine(env.WebRootPath, "House", "Images");
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }

                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var filePath = "http://" + HttpContext.Request.Host.Value + "/House/Images/" + fileName;

                        imgModel.ImageUrl = filePath;
                        imgModel.SubmittedDate = DateTime.Now;
                        context.Images.Add(imgModel);
                        await context.SaveChangesAsync();

                    }
                }

                return Ok("All the files are successfully uploaded");

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("GetImages")]
        public string[] GetImages([FromBody] ImageModel model)
        {
            
            var imageModelList = context.Images.Where(x => x.SubmittedBy == model.SubmittedBy && x.HouseId == model.HouseId && x.SectionId == model.SectionId).ToList();
            List<string> urlList = new List<string>();
            foreach (var item in imageModelList)
            {
                urlList.Add(item.ImageUrl);

            }
            return urlList.ToArray();
        }

       
        

    }
}