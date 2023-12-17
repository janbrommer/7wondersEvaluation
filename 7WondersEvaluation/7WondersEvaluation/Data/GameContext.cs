using Microsoft.EntityFrameworkCore;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }           

    public DbSet<PlayersInGame> PlayersInGame { get; set; }           

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Define composite key.
        builder.Entity<PlayersInGame>()
            .HasKey(pg => new { pg.GameId, pg.PlayerId });
    }

}


public class Game
{
    public Game(){
        PlayersInGame = new List<PlayersInGame>();
    }
    public int GameId { get; set; }
    public required string GameName { get; set; }

    public DateTime GameDate { get; set; }

    public bool IsFinished { get; set; }    

    public List<PlayersInGame> PlayersInGame { get; } = new();

    public void AddPlayer(Player player){
        PlayersInGame.Add(new PlayersInGame{ Game = this, Player = player, PositionInGame = PlayersInGame.Count});        
    }

    public void AddPlayer(Player player, int positionInGame){
        PlayersInGame.Add(new PlayersInGame{ Game = this, Player = player, PositionInGame = positionInGame});        
    }

}

public class Player
{
    public int PlayerId { get; set; }
    public required string PlayerName { get; set; }    
    
    public List<PlayersInGame> PlayersInGame { get; } = new();
}

public class PlayersInGame 
{
    public int GameId { get; set; }

    public virtual Game Game  {get; set; }
    public int PlayerId {get; set;}
    public virtual Player Player  {get; set; }

    public int PositionInGame {get; set;}    
}