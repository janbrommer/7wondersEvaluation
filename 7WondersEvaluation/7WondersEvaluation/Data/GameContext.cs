using Microsoft.EntityFrameworkCore;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }    

    public DbSet<PlayerGame> PlayerGames { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<PlayerGame>()
        .HasKey(pg => new { pg.PlayerId, pg.GameId });
}

}


public class Game
{
    public int GameId { get; set; }
    public required string GameName { get; set; }

    public DateTime GameDate { get; set; }

    public bool IsFinished { get; set; }

    public List<PlayerGame>? PlayersInGame { get; set; }
}

public class Player
{
    public int PlayerId { get; set; }
    public required string PlayerName { get; set; }

    public List<PlayerGame>? GamesPlayed { get; set; }
}

public class PlayerGame
{
    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public int PositionInGame { get; set; }
}
