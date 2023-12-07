using System;
using System.Collections.Generic;

namespace BreweryEFClasses.Models;

public partial class AddressType
{
    public int AddressTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SupplierAddress> SupplierAddresses { get; set; } = new List<SupplierAddress>();
}
