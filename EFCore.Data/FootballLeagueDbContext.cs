using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=FootballLeague; User Id=SA; Password=Kaydub23!; TrustServerCertificate=True");
        }

        // services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        //sql optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=FootballLeague;User Id=sa;Password=Jumpman23!;");
        //postgres optionsBuilder.UseNpgsql("User ID=kaycee;Password=Kaydub23!;Host=localhost;Port=1433;Database=FootballLeague;Pooling=true;");
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
}