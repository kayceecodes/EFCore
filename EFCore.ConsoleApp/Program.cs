using EFCore.Data;
using EFCore.Domain;
using EFCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.ConsoleApp;

class Program
{   
    private static FootballLeagueDbContext context = new FootballLeagueDbContext();
    
    static async Task Main(string[] args)
    {
        // var league = new League { Name = "Bundesliga" };
        // var team = new Team { Name = "Bayern Munich", League = league };
        
        // var league = new League { Name = "Serie A" };
        // await context.Leagues.AddAsync(league);
        
        // await context.AddAsync(team);
        // await context.SaveChangesAsync();

        // await AddTeamsWithLeague(league);
        // await context.SaveChangesAsync();

        // QueryFilters();

        // await UpdateRecord();
        await AddNewLeagueWithTeams();
        await AddNewTeamsWithLeague();
        await AddNewTeamWithLeagueId();
        await AddNewMatches();

        await QueryRelatedRecords();

        Console.WriteLine("Press Any Key To End....");
        Console.ReadKey();
    }

    // Note: Next go back to enter values in DB to match what Trevoir has

    static async Task FilterWithRelatedData()
    {
        // return leagues where the team names contain 'Bay'
        var leagues = context.Leagues.Where(q => q.Teams.Any(q => q.Name.Contains("Bay"))).ToListAsync();
    }

    async static Task AnnonymousProjection()
    {
        var teams = await context.Teams.Include(q => q.Coach)
        .Select(
         q =>
            new 
            {
                TeamName = q.Name,
                CoachName = q.Coach.Name       
            }   
        ).ToListAsync();
        
    }

    async static Task StronglyTypedProjection()
    {
        var teams = await context.Teams.Include(q => q.Coach).Include(q => q.League).Select(
            q =>
            new TeamDetail {
                Name = q.Name,
                CoachName = q.Coach.Name,
                LeagueName = q.League.Name
            }).ToListAsync();

        foreach(var team in teams)
            Console.WriteLine($"Team: {team.Name} | Coach: {team.CoachName} | League: {team.LeagueName}");
    }

    static async Task QueryRelatedRecords()
    {
        // Get Many Related Records
        //var leagues = context.Leagues.Include(q => q.Teams).ToList();
        
        // Getting grandchildren Related Records - Team -> Matches -> Home/Away Team
        var teamWithMatchesAndOpponents = context.Teams
        .Include(q => q.AwayMatches).ThenInclude(q => q.HomeTeam)
        .Include(q => q.HomeMatches).ThenInclude(q => q.AwayTeam)
        .FirstOrDefault(q => q.Id == 1);

        // Get Teams with filters
        var teamsWithTheirCoach = await context.Teams
        .Where(q => q.AwayMatches.Count > 0)
        .Include(q => q.Coach)
        .ToListAsync();
    }

    static async Task AddNewLeagueWithTeams()
    {
        var teams = new List<Team>
        {
            new Team
            {
                Name = "Revoli United"
            },
            new Team
            {
                Name = "Waterhouse FC"
            }
        };
        var league = new League { Name = "CIFA", Teams = teams };
        await context.AddAsync(league);
        await context.SaveChangesAsync();
    }

    static async Task AddNewMatches()
    {
        var matches = new List<Match>
        {
            new Match
            {
                AwayTeamId = 1, HomeTeamId = 2, Date = new DateTime(2021, 10, 28)
            },
            new Match 
            {
                AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
            },
            new Match 
            {
                AwayTeamId = 8, HomeTeamId = 7, Date = DateTime.Now
            }
        };
        await context.AddRangeAsync(matches);
        await context.SaveChangesAsync();
    }

    static async Task AddNewTeamsWithLeague()
    {
        var league = new League { Name = "Bundesliga" };
        var team = new Team { Name = "Bayern Munich", League = league };
        await context.AddAsync(team);
        await context.SaveChangesAsync();
    }
    static async Task AddNewTeamWithLeagueId()
    {
        var team = new Team { Name = "Fiorentina", LeagueId = 8 };
        await context.AddAsync(team);

        await context.SaveChangesAsync();
    }
    private static async Task AddNewCoach()
    {
        var coach1 = new Coach { Name = "Jose Mourinho", TeamId = 3 };

        await context.AddAsync(coach1);

        var coach2 = new Coach { Name = "Antonio Conte" };

        await context.AddAsync(coach2);
        await context.SaveChangesAsync();
    }

    // private static async Task TrackingVsNoTracking()
    // {
    //     var withTracking = await context.Teams.FirstOrDefaultAsync(q => q.Id == 2);
    //     var withNoTracking = await context.Teams.AsNoTracking().FirstOrDefaultAsync(q => q.Id == 8);
        
    //     withTracking.Name = "Inter Milan";
    //     withNoTracking.Name = "Rivoli United";

    //     var entriesBeforeSave = context.ChangeTracker.Entries();

    //     await context.SaveChangesAsync();

    //     var entriesAfterSave = context.ChangeTracker.Entries();
    // }

    // static async Task QueryFilters()
    // {
    //     Console.Write($"Enter League Name (Or Part Of): ");
    //     var leagueName = Console.ReadLine();
    //     var exactMatches = await context.Leagues.Where(q => q.Name.Equals(leagueName)).ToListAsync();

    //     foreach (var league in exactMatches)
    //         Console.WriteLine($"{league.Id} - {league.Name}");

    //     var partialMatches = await context.Leagues.Where(q => EF.Functions.Like(q.Name, $"%{leagueName}%")).ToListAsync();
    //     foreach (var league in partialMatches)
    //         Console.WriteLine($"{league.Id} - {league.Name}");
    // }


    // private static async Task GetRecord()
    // {
    //     var league = await context.Leagues.FindAsync(3);
    //     Console.WriteLine($"{league.Id} - {league.Name}");
    // }

    // private static async Task UpdateRecord()
    // {
    //     // Retrieve Record
    //     var league = await context.Leagues.FindAsync(3);
    //     // Make Changes
    //     league.Name = "Scottish Premiership";

    //     await context.SaveChangesAsync();

    //     await GetRecord();
    // }

    // static async Task AdditionalExecutionMethods()
    // {
    //     // (METHODNAME)OrDefault will return null if it can't find anything instead of causing an error, i.e. methods without OrDefault option
    //     // Tip - FirstOrDefault also takes a condition which could simplify 
    //     var first = context.Leagues.FirstOrDefaultAsync(q => q.Name.Contains("A"));
        
    //     //Single() will return one single value found under a condition. Will result in an error if more than one matches condition
    //     // var single = context.Leagues.SingleOrDefault(q => q.Name.Contains("A"));

    //     // var count = await context.Leagues.CountAsync();
    //     // var max = await context.Leagues.MaxAsync();
    //     // var min = await context.Leagues.MinAsync();

    //     var league = await context.Leagues.FindAsync(1);
    // }



    // static async Task AddTeamsWithLeague(League league)
    // {
    //     var teams = new List<Team>()
    //     {
    //         new Team
    //         {
    //             Name = "Juventus",
    //             LeagueId = league.Id
    //         },
    //         new Team
    //         {
    //             Name = "AC Milan",
    //             LeagueId = league.Id
    //         },
    //         new Team
    //         {
    //             Name = "AS Roma",
    //             League = league
    //         }
    //     };

    //     await context.AddRangeAsync(teams);
    // }
}   
