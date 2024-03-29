using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _7WondersEvaluation.Pages;

public class ResultGameModel : PageModel
{
    private readonly ILogger<OpenGamesModel> _logger;
    public Game? ThisGame { get; set; }

    GameContext _context;

    public ResultGameModel(ILogger<OpenGamesModel> logger, GameContext context)
    {
        _logger = logger;
        _context = context;                
    }

    public async Task OnGetAsync(int GameId)
    {
        CalcVictoryPoints calcVictoryPoints = new CalcVictoryPoints();
        // Load the list of games from the database
        ThisGame = await _context.Games.Where(g => g.GameId == GameId).Include(g => g.PlayersInGame).ThenInclude(pg => pg.Evaluation).Include(g => g.PlayersInGame).ThenInclude(pg => pg.Player).Include(g => g.PlayersInGame).ThenInclude(pg => pg.PlayerOutlay).AsTracking().FirstAsync();
        if (ThisGame != null)
        {
            foreach (PlayersInGame pig in ThisGame.PlayersInGame)
            {
                int PositionLeft = ThisGame.GetPositionLeft(pig.PositionInGame);
                int PositionRight = ThisGame.GetPositionRight(pig.PositionInGame);
                PlayersInGame? playersInGameLeft = ThisGame.PlayersInGame.FirstOrDefault(pos => pos.PositionInGame == PositionLeft);
                PlayersInGame? playersInGameRight = ThisGame.PlayersInGame.FirstOrDefault(pos => pos.PositionInGame == PositionRight);
                if (pig.Evaluation != null && playersInGameLeft != null && playersInGameRight != null)
                {
                    pig.Evaluation.Violet = calcVictoryPoints.calcViolet(pig, playersInGameLeft, playersInGameRight);
                }
            }
            ThisGame.IsFinished = true;
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Game not found");
        }
    }
}



