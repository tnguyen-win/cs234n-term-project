//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BreweryEFClasses.Models;

//namespace BreweryRESTAPI.Controllers {
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SupplierAddressesController : ControllerBase {
//        private readonly BitsContext _context;

//        public SupplierAddressesController(BitsContext context) => _context = context;

//        // GET: api/SupplierAddresses
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<SupplierAddress>>> GetSupplierAddresses() {
//            if (_context.SupplierAddresses == null) return NotFound();

//            return await _context.SupplierAddresses.ToListAsync();
//        }

//        // GET: api/SupplierAddresses/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<SupplierAddress>> GetSupplierAddresses(Tuple<int, int, int> id) {
//            if (_context.SupplierAddresses == null) return NotFound();

//            var supplier_addresses = await _context.SupplierAddresses.FindAsync(id);

//            if (supplier_addresses == null) return NotFound();

//            return supplier_addresses;
//        }

//        // PUT: api/SupplierAddresses/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutSupplierAddresses(Tuple<int, int, int> id, SupplierAddress supplier_addresses) {
//            if (id.Item1 != supplier_addresses.SupplierId && id.Item2 != supplier_addresses.AddressId && id.Item3 != supplier_addresses.AddressTypeId) return BadRequest();

//            _context.Entry(supplier_addresses).State = EntityState.Modified;

//            try {
//                await _context.SaveChangesAsync();
//            } catch (DbUpdateConcurrencyException) {
//                if (!SupplierAddressesExists(id)) return NotFound();
//                else throw;
//            }

//            return NoContent();
//        }

//        // POST: api/SupplierAddresses
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<SupplierAddress>> PostSupplierAddresses(SupplierAddress supplier_addresses) {
//            if (_context.SupplierAddresses == null) return Problem("Entity set 'BreweryContext.SupplierAddress'  is null.");

//            _context.SupplierAddresses.Add(supplier_addresses);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSupplierAddress", new { id = supplier_addresses.SupplierId }, supplier_addresses);
//        }

//        // DELETE: api/SupplierAddresses/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSupplierAddresses(Tuple<int, int, int> id) {
//            if (_context.SupplierAddresses == null) return NotFound();

//            var supplier_addresses = await _context.SupplierAddresses.FindAsync(id);

//            if (supplier_addresses == null) return NotFound();

//            _context.SupplierAddresses.Remove(supplier_addresses);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool SupplierAddressesExists(Tuple<int, int, int> id) => (_context.SupplierAddresses?.Any(e => e.SupplierId == id.Item1 && e.AddressId == id.Item2 && e.AddressTypeId == id.Item3)).GetValueOrDefault();
//    }
//}
