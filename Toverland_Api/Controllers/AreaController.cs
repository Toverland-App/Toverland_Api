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
        public ActionResult<IEnumerable<AreaWithAttractionsDto>> Get()
        {
            var areas = _context.Areas
                .Include(a => a.Attractions)
                .Select(a => new AreaWithAttractionsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Attractions = a.Attractions.Select(at => new AttractionDto
                    {
                        Id = at.Id,
                        Name = at.Name,
                        MinHeight = at.MinHeight,
                        Description = at.Description,
                        OpeningTime = at.OpeningTime,
                        ClosingTime = at.ClosingTime,
                        Capacity = at.Capacity,
                        QueueSpeed = at.QueueSpeed,
                        QueueLength = at.QueueLength
                    }).ToList()
                })
                .ToList();

            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<AreaWithAttractionsDto> Get(int id)
        {
            var area = _context.Areas
                .Include(a => a.Attractions)
                .FirstOrDefault(a => a.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            var areaDto = new AreaWithAttractionsDto
            {
                Id = area.Id,
                Name = area.Name,
                Size = area.Size,
                Attractions = area.Attractions.Select(a => new AttractionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MinHeight = a.MinHeight,
                    Description = a.Description,
                    OpeningTime = a.OpeningTime,
                    ClosingTime = a.ClosingTime,
                    Capacity = a.Capacity,
                    QueueSpeed = a.QueueSpeed,
                    QueueLength = a.QueueLength
                }).ToList()
            };

            return Ok(areaDto);
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
