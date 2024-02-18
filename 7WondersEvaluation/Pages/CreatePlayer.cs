using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CreatePlayerModel : PageModel
{
    private readonly GameContext _context;

    [BindProperty]
    public Player NewPlayer { get; set; } = default!;

    public CreatePlayerModel(GameContext context)
    {
        _context = context;
    }


    public IActionResult OnGet()
    {
        NewPlayer = new Player
        {
            PlayerName = "New Player", // Set the default name of the game
        };
        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _context.Players.Add(NewPlayer);
            _context.SaveChanges(); // Save the new game to the database
            return RedirectToPage("Games"); // Redirect to the list of open games
        }

        return Page(); // Return to the form if there are validation errors
    }
}
