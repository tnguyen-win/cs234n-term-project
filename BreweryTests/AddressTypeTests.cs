using NUnit.Framework;
using BreweryEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTests {
    [TestFixture]
    public class AddressTypeTests {
        BitsContext dbContext;
        AddressType? at;
        List<AddressType>? address_types;

        [SetUp]
        public void Setup() => dbContext = new BitsContext();

        [Test]
        public void GetAllTest() {
            address_types = dbContext.AddressTypes.OrderBy(at => at.Name).ToList();
            Assert.AreEqual(3, address_types.Count);
            Assert.AreEqual("billing", address_types[0].Name);
            PrintAll(address_types);
        }

        [Test]
        public void GetByPrimaryKeyTest() {
            at = dbContext.AddressTypes.Find(2);
            Assert.IsNotNull(at);
            Assert.AreEqual("mailing", at?.Name);
            Console.WriteLine(at);
        }

        [Test]
        public void GetUsingWhere() {
            address_types = dbContext.AddressTypes.Where(s => s.Name.Equals("shipping")).OrderBy(s => s.Name).ToList();
            Assert.AreEqual(1, address_types.Count);
            Assert.AreEqual("shipping", address_types[0].Name);
            PrintAll(address_types);
        }

        [Test]
        public void GetWithSupplierAddressTest() {
            var supplier_addresses = dbContext.SupplierAddresses.ToList();
            at = dbContext.AddressTypes.Include("SupplierAddresses").Where(at => at.AddressTypeId == 2).SingleOrDefault();
            Assert.IsNotNull(at);
            Assert.AreEqual("mailing", at?.Name);
            Assert.AreEqual(12, supplier_addresses.Count);
            Console.WriteLine(at);
        }

        [Test]
        public void GetWithJoinTest() {
            var address_types = dbContext.AddressTypes.Join(dbContext.SupplierAddresses, sa => sa.AddressTypeId, at => at.AddressTypeId, (sa, at) => new { sa.AddressTypeId, sa.Name, at.SupplierId, at.AddressId }).OrderBy(r => r.AddressTypeId).ToList();
            Assert.AreEqual(12, address_types.Count);
            foreach (var s in address_types) Console.WriteLine(s);
        }

        // CRUD1 = Run this test first (beginning)
        [Test]
        public void CRUD1_CreateTest() {
            at = new AddressType {
                AddressTypeId = 2000,
                Name = "business"
            };
            dbContext.AddressTypes.Add(at);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.AddressTypes.Find(at.AddressTypeId));
        }

        // CRUD2 = Run this test second (midway)
        [Test]
        public void CRUD2_UpdateTest() {
            at = dbContext.AddressTypes.Find(at.AddressTypeId);
            at.Name = "Name was changed";
            dbContext.AddressTypes.Update(at);
            dbContext.SaveChanges();
            at = dbContext.AddressTypes.Find(at.AddressTypeId);
            Assert.AreEqual("Name was changed", at?.Name);
        }

        // CRUD3 = Run this test third (last)
        [Test]
        public void CRUD3_DeleteTest() {
            at = dbContext.AddressTypes.Find(at.AddressTypeId);
            dbContext.AddressTypes.Remove(at);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.AddressTypes.Find(at.AddressTypeId));
        }

        public static void PrintAll(List<AddressType> address_types) { foreach (AddressType at in address_types) Console.WriteLine(at); }
    }
}
