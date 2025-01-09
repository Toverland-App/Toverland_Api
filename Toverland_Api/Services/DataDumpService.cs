using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Toverland_Api.Data;
using Toverland_Api.Models;

namespace Toverland_Api.Services
{
    public class DataDumpService
    {
        private readonly ApplicationDbContext _context;

        public DataDumpService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DumpDataAsync(string filePath)
        {
            var areas = await _context.Areas.ToListAsync();
            var attractions = await _context.Attractions.ToListAsync();
            var maintenances = await _context.Maintenances.ToListAsync();
            var employees = await _context.Employees.ToListAsync();
            var visitorCounts = await _context.VisitorCounts.ToListAsync();

            var data = new
            {
                Areas = areas,
                Attractions = attractions,
                Maintenances = maintenances,
                Employees = employees,
                VisitorCounts = visitorCounts
            };

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task TruncateTablesAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [VisitorCounts]");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Maintenances]");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Attractions]");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Employees]");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM [Areas]");
        }

    }
}

