using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Toverland_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Maintenance
        [HttpGet]
        public ActionResult<IEnumerable<Maintenance>> Get()
        {
            return Ok(_context.Maintenances.ToList());
        }

        // GET: api/Maintenance/5
        [HttpGet("{id}")]
        public ActionResult<Maintenance> Get(int id)
        {
            var maintenance = _context.Maintenances.Find(id);
            if (maintenance == null)
            {
                return NotFound();
            }
            return Ok(maintenance);
        }

        // POST: api/Maintenance
        [HttpPost]
        public ActionResult<Maintenance> Post([FromBody] Maintenance maintenance)
        {
            if (maintenance == null)
            {
                return BadRequest("Maintenance is null.");
            }

            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = maintenance.Id }, maintenance);
        }

        // PUT: api/Maintenance/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Maintenance maintenance)
        {
            if (id != maintenance.Id)
            {
                return BadRequest("Maintenance ID mismatch.");
            }

            var existingMaintenance = _context.Maintenances.Find(id);
            if (existingMaintenance == null)
            {
                return NotFound();
            }

            existingMaintenance.Description = maintenance.Description;
            existingMaintenance.Date = maintenance.Date;
            existingMaintenance.Status = maintenance.Status;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Maintenance/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var maintenance = _context.Maintenances.Find(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            _context.Maintenances.Remove(maintenance);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
