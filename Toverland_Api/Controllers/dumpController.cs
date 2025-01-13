using Microsoft.AspNetCore.Mvc;
using Toverland_Api.Services;

namespace Toverland_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataDumpController : ControllerBase
    {
        private readonly DataDumpService _dataDumpService;

        public DataDumpController(DataDumpService dataDumpService)
        {
            _dataDumpService = dataDumpService;
        }

        // POST: api/DataDump/dump
        [HttpPost("dump")]
        public async Task<IActionResult> DumpData([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("File path is required.");
            }

            await _dataDumpService.DumpDataAsync(filePath);
            return Ok($"Data dumped to {filePath}");
        }

        // POST: api/DataDump/truncate
        [HttpPost("truncate")]
        public async Task<IActionResult> TruncateTables()
        {
            await _dataDumpService.TruncateTablesAsync();
            return Ok("Tables truncated successfully.");
        }
    }
}
