using System;
using System.Collections.Generic;

namespace BreweryEFClasses.Models;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
