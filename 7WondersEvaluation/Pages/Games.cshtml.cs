using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _7WondersEvaluation.Pages;

public class OpenGamesModel : PageModel
{
    private readonly ILogger<OpenGamesModel> _logger;
    public List<Game> OpenGames { get; set; }

    GameContext _context;

    public OpenGamesModel(ILogger<OpenGamesModel> logger, GameContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task OnGetAsync()
    {
        // Load the list of games from the database
        OpenGames = await _context.Games.Where(g => g.IsFinished == false).Include(g => g.PlayersInGame).ThenInclude(pg => pg.Evaluation).ToListAsync();
    }
    public async Task OnPostMyDeleteAsync(int GameId)
    {
        Game game = await _context.Games.Where(g => g.GameId == GameId).Include(pg => pg.PlayersInGame).ThenInclude(pg => pg.Evaluation).Include(pg => pg.PlayersInGame).ThenInclude(pg => pg.PlayerOutlay).FirstOrDefaultAsync();
        _context.Remove(game);
        Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
        _context.SaveChangesAsync();
        OpenGames = await _context.Games.Where(g => g.IsFinished == false).Include(g => g.PlayersInGame).ThenInclude(pg => pg.Evaluation).ToListAsync();
        in eins = 1 + 1+1 ;
    }
}
