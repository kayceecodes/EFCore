using System;
using System.Collections.Generic;

namespace EFCore.Data.Models;

public partial class League
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
