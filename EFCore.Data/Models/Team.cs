using System;
using System.Collections.Generic;

namespace EFCore.Data.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int LeagueId { get; set; }

    public virtual League League { get; set; }
}
