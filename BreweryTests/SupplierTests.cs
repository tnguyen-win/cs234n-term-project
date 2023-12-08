using NUnit.Framework;
using BreweryEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTests {
    [TestFixture]
    public class SupplierTests {
        BitsContext dbContext;
        Supplier? s;
        List<Supplier>? suppliers;

        [SetUp]
        public void Setup() => dbContext = new BitsContext();

        [Test]
        public void GetAllTest() {
            suppliers = dbContext.Suppliers.OrderBy(s => s.Name).ToList();
            Assert.AreEqual(6, suppliers.Count);
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);
            PrintAll(suppliers);
        }

        [Test]
        public void GetByPrimaryKeyTest() {
            s = dbContext.Suppliers.Find(2);
            Assert.IsNotNull(s);
            Assert.AreEqual("Malteurop Malting Company", s?.Name);
            Console.WriteLine(s);
        }

        [Test]
        public void GetUsingWhere() {
            suppliers = dbContext.Suppliers.Where(s => s.Phone.Equals("8004967732")).OrderBy(s => s.Phone).ToList();
            Assert.AreEqual(1, suppliers.Count);
            Assert.AreEqual("Country Malt Group", suppliers[0].Name);
            PrintAll(suppliers);
        }

        [Test]
        public void GetWithIngredientInventoryAdditionsTest() {
            var ingredient_inventory_additions = dbContext.IngredientInventoryAdditions.ToList();
            s = dbContext.Suppliers.Include("IngredientInventoryAdditions").Where(s => s.SupplierId == 4).SingleOrDefault();
            Assert.IsNotNull(s);
            Assert.AreEqual("Hopsteiner", s?.Name);
            Assert.AreEqual(35, ingredient_inventory_additions.Count);
            Console.WriteLine(s);
        }

        [Test]
        public void GetWithSupplierAddressTest() {
            var suppliers_addresses = dbContext.SupplierAddresses.ToList();
            s = dbContext.Suppliers.Include("SupplierAddresses").Where(s => s.SupplierId == 5).SingleOrDefault();
            Assert.IsNotNull(s);
            Assert.AreEqual("John I. Haas, Inc.", s?.Name);
            Assert.AreEqual(12, suppliers_addresses.Count);
            Console.WriteLine(s);
        }

        [Test]
        public void GetWithJoinTest() {
            var suppliers = dbContext.Suppliers.Join(dbContext.IngredientInventoryAdditions, s => s.SupplierId, iva => iva.SupplierId, (s, iva) => new { s.SupplierId, s.Name, s.Phone, s.Email, s.Website, s.ContactFirstName, s.ContactLastName, s.ContactPhone, s.ContactEmail, s.Note, iva.IngredientInventoryAdditionId, iva.IngredientId, iva.OrderDate, iva.EstimatedDeliveryDate, iva.TransactionDate, iva.Quantity, iva.QuantityRemaining, iva.UnitCost }).OrderBy(r => r.SupplierId).ToList();
            Assert.AreEqual(35, suppliers.Count);
            foreach (var s in suppliers) Console.WriteLine(s);
        }

        // CRUD1 = Run this test first (beginning)
        [Test]
        public void CRUD1_CreateTest() {
            s = new Supplier {
                SupplierId = 4000,
                Name = "Target",
                Phone = "12345678910",
                Email = "inquiries@target.com",
                Website = "https://walmart.com",
                ContactFirstName = "Jane",
                ContactLastName = "Doe",
                ContactPhone = "110987654321",
                ContactEmail = "janedoe@target.com",
                Note = null
            };
            dbContext.Suppliers.Add(s);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Suppliers.Find(s.SupplierId));
        }

        // CRUD2 = Run this test second (midway)
        [Test]
        public void CRUD2_UpdateTest() {
            s = dbContext.Suppliers.Find(s.SupplierId);
            s.Name = "Name was changed";
            dbContext.Suppliers.Update(s);
            dbContext.SaveChanges();
            s = dbContext.Suppliers.Find(s.SupplierId);
            Assert.AreEqual("Name was changed", s?.Name);
        }

        // CRUD3 = Run this test third (last)
        [Test]
        public void CRUD3_DeleteTest() {
            s = dbContext.Suppliers.Find(s.SupplierId);
            dbContext.Suppliers.Remove(s);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Suppliers.Find(s.SupplierId));
        }

        public static void PrintAll(List<Supplier> suppliers) { foreach (Supplier s in suppliers) Console.WriteLine(s); }
    }
}
