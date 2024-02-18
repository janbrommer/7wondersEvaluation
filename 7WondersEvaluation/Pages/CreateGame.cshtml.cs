using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateGameModel : PageModel
{
    private readonly GameContext _context;

    public int PlayerCount;

    [BindProperty]
    public Game? NewGame { get; set; }

    [BindProperty]
    public List<Player> Players { get; set; }

    public CreateGameModel(GameContext context)
    {
        _context = context;
        PlayerCount = 3;
        NewGame = null;
        Players = new List<Player>();
    }

    public IActionResult OnGet()
    {
        NewGame = new Game
        {
            GameName = "New Game", // Set the default name of the game
            GameDate = DateTime.Now, // Use the current date and time
            IsFinished = false, // Set IsFinished to false                       
        };

        Players = new List<Player>();
        for (int i = 0; i < PlayerCount; i++)
        {
            NewGame.PlayersInGame.Add(new PlayersInGame { Player = new Player { PlayerName = "" }, PositionInGame = i , Game = NewGame});
            Players.Add(new Player { PlayerName = "" });
        }

        return Page();
    }

    public IActionResult OnPost()
    {

        if (ModelState.IsValid)
        {
            if (NewGame != null && Players != null)
            {
                Players.ForEach(NewGame.AddPlayer);
                _context.Games.Add(NewGame);
                _context.SaveChanges(); // Save the new game to the database
                return RedirectToPage("Games"); // Redirect to the list of open games
            }
        }
        else
        {
            var errors = ModelState.Values
                      .SelectMany(x => x.Errors)
                      .Where(e => e != null && !string.IsNullOrEmpty(e.ErrorMessage))
                      .ToList();

            Console.WriteLine(errors);
        }

        return Page(); // Return to the form if there are validation errors
    }
}
