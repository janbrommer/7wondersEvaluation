using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreateGameModel : PageModel
{
    private readonly GameContext _context;

    public CreateGameModel(GameContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Game NewGame { get; set; }

    public IActionResult OnGet()
    {
        NewGame = new Game
        {
            GameName = "New Game", // Set the default name of the game
            GameDate = DateTime.Now, // Use the current date and time
            IsFinished = false // Set IsFinished to false
        };
        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _context.Games.Add(NewGame);
            _context.SaveChanges(); // Save the new game to the database
            return RedirectToPage("Games"); // Redirect to the list of open games
        }

        return Page(); // Return to the form if there are validation errors
    }
}
