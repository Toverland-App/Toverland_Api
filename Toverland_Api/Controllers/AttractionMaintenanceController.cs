using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Toverland_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionMaintenanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttractionMaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/AttractionMaintenance/AddMaintenance
        [HttpPost("AddMaintenance")]
        public ActionResult AddMaintenance(int attractionId, [FromBody] Maintenance maintenance)
        {
            var attraction = _context.Attractions.Find(attractionId);
            if (attraction == null)
            {
                return NotFound("Attraction not found.");
            }

            if (maintenance == null)
            {
                return BadRequest("Maintenance is null.");
            }

            // Check if a maintenance record already exists for the given attraction and description
            var existingMaintenance = _context.Maintenances
                .FirstOrDefault(m => m.AttractionId == attractionId && m.Description == maintenance.Description);
            if (existingMaintenance != null)
            {
                return Conflict("A maintenance record with the same description already exists for this attraction.");
            }

            // Ensure only the necessary fields are set
            maintenance.AttractionId = attractionId;
            maintenance.Attraction = null; // Ensure the attraction is not included in the maintenance entity

            _context.Maintenances.Add(maintenance);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle the concurrency exception
                return Conflict(new { message = "Concurrency conflict occurred.", details = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                // Handle the update exception
                return StatusCode(500, new { message = "An error occurred while saving the entity changes.", details = ex.InnerException?.Message });
            }

            return Ok("Maintenance added to attraction.");
        }

        // DELETE: api/AttractionMaintenance/RemoveMaintenance
        [HttpDelete("RemoveMaintenance")]
        public ActionResult RemoveMaintenance(int attractionId, int maintenanceId)
        {
            var attraction = _context.Attractions.Find(attractionId);
            if (attraction == null)
            {
                return NotFound("Attraction not found.");
            }

            var maintenance = _context.Maintenances.Find(maintenanceId);
            if (maintenance == null || maintenance.AttractionId != attractionId)
            {
                return NotFound("Maintenance not found or does not belong to the specified attraction.");
            }

            _context.Maintenances.Remove(maintenance);
            _context.SaveChanges();

            return Ok("Maintenance removed from attraction.");
        }

        // GET: api/AttractionMaintenance/GetMaintenance
        [HttpGet("GetMaintenance")]
        public ActionResult GetMaintenance(int attractionId)
        {
            var attraction = _context.Attractions.Find(attractionId);
            if (attraction == null)
            {
                return NotFound("Attraction not found.");
            }

            var maintenances = _context.Maintenances
                .Where(m => m.AttractionId == attractionId)
                .ToList();

            if (!maintenances.Any())
            {
                return NotFound("No maintenance records found for this attraction.");
            }

            return Ok(maintenances);
        }

        // GET: api/AttractionMaintenance/GetAllMaintenances
        [HttpGet("GetAllMaintenances")]
        public ActionResult GetAllMaintenances()
        {
            var maintenances = _context.Maintenances.ToList();

            if (!maintenances.Any())
            {
                return NotFound("No maintenance records found.");
            }

            return Ok(maintenances);
        }
    }
}
