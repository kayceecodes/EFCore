using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.Data;

public class FootballLeagueDbContext : DbContext
{
    // private readonly IConfiguration _config;

    // public FootballLeagueDbContext(IConfiguration config)
    // {
    //     _config = config;
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if(!optionsBuilder.IsConfigured)
        {
            // var connectionString = _config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=FootballLeague; User Id=SA; Password=Kaydub23!; TrustServerCertificate=True")
            .LogTo(Console.WriteLine, new [] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging();

        }

        // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        //sql optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=FootballLeague;User Id=sa;Password=Jumpman23!;");
        //postgres optionsBuilder.UseNpgsql("User ID=kaycee;Password=Kaydub23!;Host=localhost;Port=1433;Database=FootballLeague;Pooling=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
        .HasMany(m => m.HomeMatches)
        .WithOne(m => m. HomeTeam)
        .HasForeignKey(m => m.HomeTeamId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
        .HasMany(m => m.AwayMatches)
        .WithOne(m => m.AwayTeam)
        .HasForeignKey(m => m.AwayTeam)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Match> Matches { get; set; }
}