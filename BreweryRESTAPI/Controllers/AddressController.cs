using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryEFClasses.Models;

namespace BreweryRESTAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase {
        private readonly BitsContext _context;

        public AddressesController(BitsContext context) => _context = context;

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses() {
            if (_context.Addresses == null) return NotFound();

            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddresses(int id) {
            if (_context.Addresses == null) return NotFound();

            var addresses = await _context.Addresses.FindAsync(id);

            if (addresses == null) return NotFound();

            return addresses;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddresses(int id, Address addresses) {
            if (id != addresses.AddressId) return BadRequest();

            _context.Entry(addresses).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AddressesExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddresses(Address addresses) {
            if (_context.Addresses == null) return Problem("Entity set 'BreweryContext.Addresses'  is null.");

            _context.Addresses.Add(addresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddresses", new { id = addresses.AddressId }, addresses);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddresses(int id) {
            if (_context.Addresses == null) return NotFound();

            var addresses = await _context.Addresses.FindAsync(id);

            if (addresses == null) return NotFound();

            _context.Addresses.Remove(addresses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressesExists(int id) => (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
    }
}
