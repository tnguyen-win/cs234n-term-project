using NUnit.Framework;
using BreweryEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTests {
    [TestFixture]
    public class AddressTests {
        BitsContext dbContext;
        Address? a;
        List<Address>? addresses;

        [SetUp]
        public void Setup() => dbContext = new BitsContext();

        [Test]
        public void GetAllTest() {
            addresses = dbContext.Addresses.OrderBy(a => a.AddressId).ToList();
            Assert.AreEqual(7, addresses.Count);
            Assert.AreEqual("800 West 1st Ave", addresses[0].StreetLine1);
            PrintAll(addresses);
        }

        [Test]
        public void GetByPrimaryKeyTest() {
            a = dbContext.Addresses.Find(2);
            Assert.IsNotNull(a);
            Assert.AreEqual("3830 W. Grant Street", a?.StreetLine1);
            Console.WriteLine(a);
        }

        [Test]
        public void GetUsingWhere() {
            addresses = dbContext.Addresses.Where(a => a.State.Equals("WI")).OrderBy(s => s.AddressId).ToList();
            Assert.AreEqual(1, addresses.Count);
            Assert.AreEqual("3830 W. Grant Street", addresses[0].StreetLine1);
            PrintAll(addresses);
        }

        [Test]
        public void GetWithSupplierAddressTest() {
            var supplier_addresses = dbContext.SupplierAddresses.ToList();
            a = dbContext.Addresses.Include("SupplierAddresses").Where(a => a.AddressId == 3).SingleOrDefault();
            Assert.IsNotNull(a);
            Assert.AreEqual("2501 Kotobuki Way", a?.StreetLine1);
            Assert.AreEqual(12, supplier_addresses.Count);
            Console.WriteLine(a);
        }

        [Test]
        public void GetWithJoinTest() {
            var addresses = dbContext.Addresses.Join(dbContext.SupplierAddresses, a => a.AddressId, sa => sa.AddressTypeId, (a, sa) => new { a.AddressId, a.StreetLine1, a.StreetLine2, a.City, a.State, a.Zipcode, a.Country, sa.SupplierId, sa.AddressTypeId }).OrderBy(r => r.AddressId).ToList();
            Assert.AreEqual(12, addresses.Count);
            foreach (var s in addresses) Console.WriteLine(s);
        }

        // CRUD1 = Run this test first (beginning)
        [Test]
        public void CRUD1_CreateTest() {
            a = new Address {
                AddressId = 1000,
                StreetLine1 = "1",
                StreetLine2 = "2",
                City = "3",
                State = "FL",
                Zipcode = "4",
                Country = "5",
            };
            dbContext.Addresses.Add(a);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Addresses.Find(a.AddressId));
        }

        // CRUD2 = Run this test second (midway)
        [Test]
        public void CRUD2_UpdateTest() {
            a = dbContext.Addresses.Find(a.AddressId);
            a.StreetLine1 = "StreetLine1 was changed";
            dbContext.Addresses.Update(a);
            dbContext.SaveChanges();
            a = dbContext.Addresses.Find(a.AddressId);
            Assert.AreEqual("StreetLine1 was changed", a?.StreetLine1);
        }

        // CRUD3 = Run this test third (last)
        [Test]
        public void CRUD3_DeleteTest() {
            a = dbContext.Addresses.Find(a.AddressId);
            dbContext.Addresses.Remove(a);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Addresses.Find(a.AddressId));
        }

        public static void PrintAll(List<Address> addresses) { foreach (Address a in addresses) Console.WriteLine(a); }
    }
}
