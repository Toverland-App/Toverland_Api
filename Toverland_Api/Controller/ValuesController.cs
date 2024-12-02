using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Toverland_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private static List<Area> Areas = new List<Area>
        {
            new Area { Id = 1, Name = "Area 1", Size = 100.0 },
            new Area { Id = 2, Name = "Area 2", Size = 200.0 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Area>> Get()
        {
            return Ok(Areas);
        }

        [HttpGet("{id}")]
        public ActionResult<Area> Get(int id)
        {
            var area = Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            return Ok(area);
        }

        [HttpPost]
        public ActionResult<Area> Post([FromBody] Area area)
        {
            area.Id = Areas.Max(a => a.Id) + 1;
            Areas.Add(area);
            return CreatedAtAction(nameof(Get), new { id = area.Id }, area);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Area updatedArea)
        {
            var area = Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            area.Name = updatedArea.Name;
            area.Size = updatedArea.Size;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var area = Areas.FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }
            Areas.Remove(area);
            return NoContent();
        }
    }
}
