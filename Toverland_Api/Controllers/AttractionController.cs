using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Toverland_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttractionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttractionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Attraction>> Get()
        {
            return Ok(_context.Attractions.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Attraction> Get(int id)
        {
            var attraction = _context.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction == null)
            {
                return NotFound();
            }
            return Ok(attraction);
        }

        [HttpPost]
        public ActionResult<Attraction> Post([FromBody] Attraction attraction)
        {
            _context.Attractions.Add(attraction);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = attraction.Id }, attraction);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Attraction updatedAttraction)
        {
            var attraction = _context.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction == null)
            {
                return NotFound();
            }
            attraction.Name = updatedAttraction.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var attraction = _context.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction == null)
            {
                return NotFound();
            }
            _context.Attractions.Remove(attraction);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
