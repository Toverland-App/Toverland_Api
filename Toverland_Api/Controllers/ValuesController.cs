using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Toverland_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Area>> Get()
        {
            var areas = _context.Areas
                .Include(a => a.Attractions)
                .ToList();

            return Ok(areas);
        }


        [HttpGet("{id}")]
        public ActionResult<Area> Get(int id)
        {
            var area = _context.Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }

        [HttpPost]
        public ActionResult<Area> Post([FromBody] Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = area.Id }, area);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Area updatedArea)
        {
            var area = _context.Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            area.Name = updatedArea.Name;
            area.Size = updatedArea.Size;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var area = _context.Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            _context.Areas.Remove(area);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
