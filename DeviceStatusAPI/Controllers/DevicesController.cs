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
    public class DevicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }

        // POST: api/Devices
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            if (await _context.Devices.AnyAsync(d => d.DeviceName == device.DeviceName))
            {
                return Conflict("A device with this name already exists.");
            }

            device.LastUpdated = DateTime.Now;
            _context.Devices.Add(device);

            var deviceHistory = new DeviceHistory
            {
                DeviceID = device.DeviceID,
                DeviceName = device.DeviceName,
                Status = device.Status,
                LastUpdated = device.LastUpdated
            };
            _context.DeviceHistories.Add(deviceHistory);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.DeviceID }, device);
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.DeviceID)
            {
                return BadRequest();
            }

            if (await _context.Devices.AnyAsync(d => d.DeviceName == device.DeviceName && d.DeviceID != id))
            {
                return Conflict("A device with this name already exists.");
            }

            device.LastUpdated = DateTime.Now;
            _context.Entry(device).State = EntityState.Modified;

            var deviceHistory = new DeviceHistory
            {
                DeviceID = device.DeviceID,
                DeviceName = device.DeviceName,
                Status = device.Status,
                LastUpdated = device.LastUpdated
            };
            _context.DeviceHistories.Add(deviceHistory);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceID == id);
        }
    }
}
