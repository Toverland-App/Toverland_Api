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
    public class AttractionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttractionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            var attractions = await _context.Attractions
                .Include(a => a.Area) // Include related Area entity
                .ToListAsync();

            return Ok(attractions);
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
            if (attraction == null)
            {
                return BadRequest("Attraction is null.");
            }

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
