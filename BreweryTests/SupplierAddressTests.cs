using NUnit.Framework;
using BreweryEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryTests {
    [TestFixture]
    public class SupplierAddressTests {
        BitsContext dbContext;
        Supplier? s;
        Address? a;
        AddressType? at;
        SupplierAddress? sa;
        List<SupplierAddress>? suppliers_addresses;

        [SetUp]
        public void Setup() => dbContext = new BitsContext();

        [Test]
        public void GetAllTest() {
            suppliers_addresses = dbContext.SupplierAddresses.OrderBy(s => s.SupplierId).ToList();
            Assert.AreEqual(12, suppliers_addresses.Count);
            Assert.AreEqual(1, suppliers_addresses[0].AddressId);
            PrintAll(suppliers_addresses);
        }

        [Test]
        public void GetByPrimaryKeyTest() {
            sa = dbContext.SupplierAddresses.Find(1, 1, 1);
            Assert.IsNotNull(sa);
            Assert.AreEqual(1, sa?.SupplierId);
            Assert.AreEqual(1, sa?.AddressId);
            Assert.AreEqual(1, sa?.AddressTypeId);
            Console.WriteLine(sa);
        }

        [Test]
        public void GetUsingWhere() {
            suppliers_addresses = dbContext.SupplierAddresses.Where(s => s.AddressTypeId.Equals(2)).OrderBy(s => s.AddressTypeId).ToList();
            Assert.AreEqual(6, suppliers_addresses.Count);
            Assert.AreEqual(7, suppliers_addresses[5].AddressId);
            PrintAll(suppliers_addresses);
        }

        [Test]
        public void GetWithJoinTest() {
            var suppliers_addresses = dbContext.SupplierAddresses.Join(dbContext.Suppliers, sa => sa.SupplierId, s => s.SupplierId, (sa, s) => new { sa.SupplierId, sa.AddressId, sa.AddressTypeId, s.Name, s.Phone, s.Email, s.Website, s.ContactFirstName, s.ContactLastName, s.ContactPhone, s.ContactEmail, s.Note }).OrderBy(r => r.SupplierId).ToList();
            Assert.AreEqual(12, suppliers_addresses.Count);
            foreach (var sa in suppliers_addresses) Console.WriteLine(sa);
        }

        //// CRUD1 = Run this test first (beginning)
        //[Test]
        //public void CRUD1_CreateTest() {
        //    // Supplier
        //    s = new Supplier {
        //        SupplierId = 3000,
        //        Name = "Walmart",
        //        Phone = "12345678910",
        //        Email = "inquiries@walmart.com",
        //        Website = "https://walmart.com",
        //        ContactFirstName = "John",
        //        ContactLastName = "Doe",
        //        ContactPhone = "110987654321",
        //        ContactEmail = "johndoe@walmart.com",
        //        Note = null
        //    };
        //    dbContext.Suppliers.Add(s);
        //    dbContext.SaveChanges();
        //    Assert.IsNotNull(dbContext.Suppliers.Find(s.SupplierId));
        //    // Address
        //    a = new Address {
        //        AddressId = s.SupplierId,
        //        StreetLine1 = "1",
        //        StreetLine2 = "2",
        //        City = "3",
        //        State = "FL",
        //        Zipcode = "4",
        //        Country = "5",
        //    };
        //    dbContext.Addresses.Add(a);
        //    dbContext.SaveChanges();
        //    Assert.IsNotNull(dbContext.Addresses.Find(a.AddressId));
        //    // Address Type
        //    at = new AddressType {
        //        AddressTypeId = s.SupplierId,
        //        Name = "business"
        //    };
        //    dbContext.AddressTypes.Add(at);
        //    dbContext.SaveChanges();
        //    Assert.IsNotNull(dbContext.AddressTypes.Find(at.AddressTypeId));
        //    // Supplier Address
        //    sa = new SupplierAddress {
        //        SupplierId = s.SupplierId,
        //        AddressId = a.AddressId,
        //        AddressTypeId = at.AddressTypeId
        //    };
        //    dbContext.SupplierAddresses.Add(sa);
        //    dbContext.SaveChanges();
        //    Assert.IsNotNull(dbContext.SupplierAddresses.Find(s.SupplierId, a.AddressId, at.AddressTypeId));
        //}

        ////CRUD2 = Run this test second (midway)
        //[Test]
        //public void CRUD2_UpdateTest() {
        //    // Supplier
        //    s = dbContext.Suppliers.Find(s.SupplierId);
        //    s.Name = "Name was changed";
        //    dbContext.Suppliers.Update(s);
        //    dbContext.SaveChanges();
        //    s = dbContext.Suppliers.Find(s.SupplierId);
        //    Assert.AreEqual("Name was changed", s?.Name);
        //    // Address
        //    a = dbContext.Addresses.Find(a.AddressId);
        //    a.StreetLine1 = "StreetLine1 was changed";
        //    dbContext.Addresses.Update(a);
        //    dbContext.SaveChanges();
        //    a = dbContext.Addresses.Find(a.AddressId);
        //    Assert.AreEqual("StreetLine1 was changed", a?.StreetLine1);
        //    // Address Type
        //    at = dbContext.AddressTypes.Find(at.AddressTypeId);
        //    at.Name = "Name was changed";
        //    dbContext.AddressTypes.Update(at);
        //    dbContext.SaveChanges();
        //    at = dbContext.AddressTypes.Find(at.AddressTypeId);
        //    Assert.AreEqual("Name was changed", at?.Name);
        //    // Supplier Address
        //    sa = dbContext.SupplierAddresses.Find(s.SupplierId, a.AddressId, at.AddressTypeId);
        //    sa.AddressTypeId = 1;
        //    dbContext.SupplierAddresses.Update(sa);
        //    dbContext.SaveChanges();
        //    sa = dbContext.SupplierAddresses.Find(s.SupplierId, a.AddressId, at.AddressTypeId);
        //    Assert.AreEqual(1, sa?.AddressTypeId);
        //}

        //// CRUD3 = Run this test third (last)
        //[Test]
        //public void CRUD3_DeleteTest() {
        //    // Supplier
        //    s = dbContext.Suppliers.Find(s.SupplierId);
        //    dbContext.Suppliers.Remove(s);
        //    dbContext.SaveChanges();
        //    Assert.IsNull(dbContext.Suppliers.Find(s.SupplierId));
        //    // Address
        //    a = dbContext.Addresses.Find(s.SupplierId);
        //    dbContext.Addresses.Remove(a);
        //    dbContext.SaveChanges();
        //    Assert.IsNull(dbContext.Addresses.Find(s.SupplierId));
        //    // Address Type
        //    at = dbContext.AddressTypes.Find(s.SupplierId);
        //    dbContext.AddressTypes.Remove(at);
        //    dbContext.SaveChanges();
        //    Assert.IsNull(dbContext.AddressTypes.Find(at.AddressTypeId));
        //    // Supplier Address
        //    sa = dbContext.SupplierAddresses.Find(s.SupplierId, a.AddressId, at.AddressTypeId);
        //    dbContext.SupplierAddresses.Remove(sa);
        //    dbContext.SaveChanges();
        //    Assert.IsNull(dbContext.SupplierAddresses.Find(s.SupplierId, a.AddressId, at.AddressTypeId));
        //}

        public static void PrintAll(List<SupplierAddress> supplier_address) { foreach (SupplierAddress sa in supplier_address) Console.WriteLine(sa); }
    }
}
