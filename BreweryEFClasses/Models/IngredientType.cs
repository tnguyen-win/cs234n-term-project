using System;
using System.Collections.Generic;

namespace BreweryEFClasses.Models;

public partial class IngredientType
{
    public int IngredientTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
