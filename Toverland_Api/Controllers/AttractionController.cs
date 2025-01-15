using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Toverland_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttractionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AttractionController> _logger;

        public AttractionController(ApplicationDbContext context, ILogger<AttractionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            _logger.LogInformation("Fetching all attractions from the database.");

            var attractions = await _context.Attractions
                .Include(a => a.Area) // Include related Area entity
                .ToListAsync();

            _logger.LogInformation("Fetched {Count} attractions from the database.", attractions.Count);

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
                _logger.LogWarning("Received null attraction in POST request.");
                return BadRequest("Attraction is null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid model state in POST request: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new attraction: {@Attraction}", attraction);

            var newAttraction = new Attraction(
                attraction.Name,
                attraction.MinHeight,
                attraction.AreaId,
                attraction.Description,
                attraction.OpeningTime,
                attraction.ClosingTime,
                attraction.Capacity,
                attraction.QueueSpeed,
                attraction.QueueLength,
                attraction.Image
            );

            _context.Attractions.Add(newAttraction);
            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Attraction created with ID: {Id}", newAttraction.Id);
                return CreatedAtAction(nameof(Get), new { id = newAttraction.Id }, newAttraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the new attraction.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("withId")]
        public ActionResult<Attraction> PostWithId([FromBody] Attraction attraction)
        {
            if (attraction == null)
            {
                _logger.LogWarning("Received null attraction in POST request.");
                return BadRequest("Attraction is null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid model state in POST request: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new attraction with ID: {@Attraction}", attraction);

            var newAttraction = new Attraction(
                attraction.Id,
                attraction.Name,
                attraction.MinHeight,
                attraction.AreaId,
                attraction.Description,
                attraction.OpeningTime,
                attraction.ClosingTime,
                attraction.Capacity,
                attraction.QueueSpeed,
                attraction.QueueLength,
                attraction.Image
            );

            _context.Attractions.Add(newAttraction);
            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Attraction created with ID: {Id}", newAttraction.Id);
                return CreatedAtAction(nameof(Get), new { id = newAttraction.Id }, newAttraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the new attraction.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Attraction updatedAttraction)
        {
            if (updatedAttraction == null)
            {
                _logger.LogWarning("Received null updated attraction in PUT request.");
                return BadRequest("Updated attraction is null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid model state in PUT request: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var attraction = _context.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction == null)
            {
                _logger.LogWarning("Attraction with ID {Id} not found in PUT request.", id);
                return NotFound();
            }

            _logger.LogInformation("Updating attraction with ID: {Id}", id);

            // Update properties with provided values or retain old values
            attraction.Name = updatedAttraction.Name ?? attraction.Name;
            attraction.MinHeight = updatedAttraction.MinHeight ?? attraction.MinHeight;
            attraction.AreaId = updatedAttraction.AreaId != 0 ? updatedAttraction.AreaId : attraction.AreaId;
            attraction.Description = updatedAttraction.Description ?? attraction.Description;
            attraction.OpeningTime = updatedAttraction.OpeningTime ?? attraction.OpeningTime;
            attraction.ClosingTime = updatedAttraction.ClosingTime ?? attraction.ClosingTime;
            attraction.Capacity = updatedAttraction.Capacity ?? attraction.Capacity;
            attraction.QueueSpeed = updatedAttraction.QueueSpeed ?? attraction.QueueSpeed;
            attraction.QueueLength = updatedAttraction.QueueLength ?? attraction.QueueLength;
            attraction.Image = updatedAttraction.Image ?? attraction.Image;

            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Attraction updated with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the attraction with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var attraction = _context.Attractions.FirstOrDefault(a => a.Id == id);
            if (attraction == null)
            {
                _logger.LogWarning("Attraction with ID {Id} not found in DELETE request.", id);
                return NotFound();
            }
            _context.Attractions.Remove(attraction);
            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Attraction deleted with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the attraction with ID: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
