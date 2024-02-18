using _7WondersEvaluation.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _7WondersEvaluation.Pages;

public class JoinGameModel : PageModel
{
    private readonly ILogger<OpenGamesModel> _logger;
    public Game game { get; set; } = default!;

    GameContext _context;

    public JoinGameModel(ILogger<OpenGamesModel> logger, GameContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet(int GameId)
    {
        // Load the list of games from the database
        game = _context.Games.Where(g => g.GameId == GameId).Include(game => game.PlayersInGame).ThenInclude(PlayersInGame => PlayersInGame.Player).Include(game => game.PlayersInGame).ThenInclude(PlayersInGame => PlayersInGame.Evaluation).Single();
        PlayersInGame playersInGame = _context.PlayersInGame.Where(pg => (pg.GameId == 8 && pg.PlayerId == 7)).Include(eva => eva.Evaluation).Single();

    }
}
