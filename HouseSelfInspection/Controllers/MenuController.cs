using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseSelfInspection;
using HouseSelfInspection.Models;

namespace HouseSelfInspection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MenuController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public IEnumerable<MenuModel> GetMenus()
        {
            return _context.Menus;
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuModel = await _context.Menus.FindAsync(id);

            if (menuModel == null)
            {
                return NotFound();
            }

            return Ok(menuModel);
        }

        // GET: api/Menu/5
        [HttpGet("GetMenuByRoleId/{RoleId}")]
        public async Task<IActionResult> GetMenuByRoleId([FromRoute] int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var roleMenus = _context.RoleMenus.Where(x =>x.RoleId == RoleId).Select(x => x.MenuId).ToList();
            var menus = from menu in _context.Set<MenuModel>()
                        join role in _context.Set<RoleMenuModel>()
                         on menu.MenuId equals role.MenuId
                        where role.RoleId == RoleId
                        orderby menu.MenuId
                        select new { menu };

            if (menus == null)
            {
                return NotFound();
            }

            List<MenuModel> menuList = new List<MenuModel>();
            foreach (var item in menus)
            {
                menuList.Add(item.menu);
            }

            return Ok(menuList);
        }

        // PUT: api/Menu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu([FromRoute] int id, [FromBody] MenuModel menuModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuModel.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(menuModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menu
        [HttpPost]
        public async Task<IActionResult> PostMenu([FromBody] MenuModel menuModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Menus.Add(menuModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuModel", new { id = menuModel.MenuId }, menuModel);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuModel = await _context.Menus.FindAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menuModel);
            await _context.SaveChangesAsync();

            return Ok(menuModel);
        }

        private bool MenuModelExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}