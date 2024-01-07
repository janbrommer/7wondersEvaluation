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

    public DbSet<Evaluation> Evaluation { get; set; }          
    
    public DbSet<PlayerOutlay> PlayerOutlay { get; set; }           

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Define composite key.
        builder.Entity<PlayersInGame>()
            .HasKey(pg => new { pg.GameId, pg.PlayerId });

        builder.Entity<Evaluation>().HasKey(eva => new {eva.GameId, eva.PlayerId});
        builder.Entity<PlayerOutlay>().HasKey(plou => new {plou.GameId, plou.PlayerId});
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

    public virtual PlayerOutlay? PlayerOutlay {get; set;}
    public virtual Evaluation? Evaluation {get; set;}
}

public class PlayerOutlay
{
    
    public int GameId  { get; set; }
    public int PlayerId  { get; set; }    
    public int CountRed { get; set; }
    public int CountGreen { get; set; }
    public int CountYellow { get; set; }
    public int CountBrown { get; set; }
    public int CountGrey { get; set; }
    public int CountGild { get; set; }
    public int CountExpa { get; set; }
    public int CountWarMarker { get; set; }
    public int CountNegWarMarker { get; set; }
}

public class Evaluation
{
    public int GameId  { get; set; }
    public int PlayerId  { get; set; }    
    public int Red { get; set; } = 0;
    public int Coins { get; set; } = 0;
    public int ExpansionStages { get; set; } = 0;
    public int Blue { get; set; } = 0;
    public int Yellow { get; set; } = 0;
    public int Violet { get; set; } = 0;
    public int Green { get; set; } = 0;

    public int Sum(){
        return Red + Coins + ExpansionStages + Blue + Yellow + Violet + Green;
    }
}


