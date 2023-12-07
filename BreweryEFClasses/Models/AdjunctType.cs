using System;
using System.Collections.Generic;

namespace BreweryEFClasses.Models;

public partial class AdjunctType
{
    public int AdjunctTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Adjunct> Adjuncts { get; set; } = new List<Adjunct>();
}
