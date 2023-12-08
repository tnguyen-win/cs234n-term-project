using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreweryEFClasses.Models;

namespace BreweryRESTAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypesController : ControllerBase {
        private readonly BitsContext _context;

        public AddressTypesController(BitsContext context) => _context = context;

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddressTypes() {
            if (_context.AddressTypes == null) return NotFound();

            return await _context.AddressTypes.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddressTypes(int id) {
            if (_context.AddressTypes == null) return NotFound();

            var addressTypes = await _context.AddressTypes.FindAsync(id);

            if (addressTypes == null) return NotFound();

            return addressTypes;
        }

        // PUT: api/AddressTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressTypes(int id, AddressType addressTypes) {
            if (id != addressTypes.AddressTypeId) return BadRequest();

            _context.Entry(addressTypes).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AddressTypesExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/AddressTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddressTypes(AddressType addressTypes) {
            if (_context.AddressTypes == null) return Problem("Entity set 'BreweryContext.AddressTypes'  is null.");

            _context.AddressTypes.Add(addressTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressTypes", new { id = addressTypes.AddressTypeId }, addressTypes);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressTypes(int id) {
            if (_context.AddressTypes == null) return NotFound();

            var addressTypes = await _context.AddressTypes.FindAsync(id);

            if (addressTypes == null) return NotFound();

            _context.AddressTypes.Remove(addressTypes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressTypesExists(int id) => (_context.AddressTypes?.Any(e => e.AddressTypeId == id)).GetValueOrDefault();
    }
}
