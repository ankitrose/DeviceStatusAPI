using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceStatusAPI.Data;
using DeviceStatusAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceStatusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DeviceHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceHistory>>> GetDeviceHistory()
        {
            return await _context.DeviceHistories.ToListAsync();
        }

        // DELETE: api/DeviceHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceHistory(int id)
        {
            var history = await _context.DeviceHistories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.DeviceHistories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
