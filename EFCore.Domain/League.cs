using EFCore.Domain.Common;

namespace EFCore.Domain;

public class League : BaseDomainObject
{
    public string Name { get; set; }
    public List<Team> Teams { get; set; }    
}