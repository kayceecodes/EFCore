using EFCore.Data;
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

        Console.WriteLine("Press Any Key To End....");
        Console.ReadKey();
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