namespace EFCore.Domain;

public class Team
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int LeagueId { get; set; }
    
    public virtual League League { get; set; }

    public virtual List<Match> HomeMatches { get; set; }

    public virtual List<Match> AwayMatches { get; set; }

    public virtual Coach Coach { get; set; }
}