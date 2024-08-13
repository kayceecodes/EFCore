using System;
using EFCore.Domain.Common;

namespace EFCore.Domain;

public class Coach : BaseDomainObject
{
    public string Name { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; }
}
