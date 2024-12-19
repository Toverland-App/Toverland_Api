using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Data;
using Toverland_Api.Models;
using System.Linq;

namespace Toverland_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorCountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VisitorCountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/VisitorCount/Add
        [HttpPost("Add")]
        public ActionResult AddVisitorCount([FromBody] VisitorCount visitorCount)
        {
            if (visitorCount == null)
            {
                return BadRequest("VisitorCount is null.");
            }

            var existingVisitorCount = _context.VisitorCounts
                .FirstOrDefault(vc => vc.Date == visitorCount.Date);

            if (existingVisitorCount != null)
            {
                existingVisitorCount.Count += visitorCount.Count;
                _context.VisitorCounts.Update(existingVisitorCount);
            }
            else
            {
                _context.VisitorCounts.Add(visitorCount);
            }

            _context.SaveChanges();

            return Ok("Visitor count updated.");
        }

        // GET: api/VisitorCount/GetAll
        [HttpGet("GetAll")]
        public ActionResult GetAllVisitorCounts()
        {
            var visitorCounts = _context.VisitorCounts.ToList();
            return Ok(visitorCounts);
        }
    }
}
