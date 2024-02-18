using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json; // For the ReadFromJsonAsync method
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;



namespace _7WondersEvaluation.Pages;

public class EvaluateModel : PageModel
{

    private readonly CustomVisionClient _customVisionClient;
    private readonly ILogger<EvaluateModel> _logger;

    private readonly BlobClient _blobClient;

    public string? ImagePath { get; private set; }
    public ApiResult? Result { get; private set; }
    public Evaluation? Evaluation { get; private set; }

    GameContext _context;

    public PlayersInGame playersInGame { get; set; } = default!;

    public EvaluateModel(ILogger<EvaluateModel> logger, CustomVisionClient customVisionClient, BlobClient blobClient, GameContext context)
    {
        _logger = logger;
        _customVisionClient = customVisionClient;
        _blobClient = blobClient;
        _context = context;
    }

    public void OnGet(int playerId, int gameId)
    {
        var playersInGameQuery = _context.PlayersInGame.Where(pg => pg.GameId == gameId && pg.PlayerId == playerId);
        if (playersInGameQuery != null && playersInGameQuery.Any())
        {
            playersInGame = playersInGameQuery.First();
        }
    }



    public async Task<IActionResult> OnPostAsync(int playerId, int gameId)
    {
        try
        {
            playersInGame = _context.PlayersInGame.Where(pg => pg.GameId == gameId && pg.PlayerId == playerId).Include(pg => pg.Evaluation).Include(pg => pg.PlayerOutlay).AsTracking().First();

            if (playersInGame != null)
            {
                var image = Request.Form.Files.GetFile("image");

                if (image != null && image.Length > 0)
                {
                    var imagePath = Path.Combine("uploads", image.FileName);
                    using (var stream = new FileStream(Path.Combine("wwwroot", imagePath), FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                        stream.Position = 0;
                        await _blobClient.UploadAsync(stream, image.FileName);
                    }

                    ImagePath = "/" + imagePath;
                    Result = await _customVisionClient.PredictImageAsync(image.OpenReadStream()) ?? throw new Exception("Failed to get result.");
                    CalcVictoryPoints calcVictoryPoints = new CalcVictoryPoints(Result);
                    playersInGame.Evaluation = calcVictoryPoints.createEvaluationData(playersInGame);
                    playersInGame.PlayerOutlay = calcVictoryPoints.createPlayerOutlay(playersInGame);
                    //Console.WriteLine(Result);
                    Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
                    _context.SaveChanges();
                }
            }else{
                throw new Errors.PlayerNotFoundException("Player not found");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return Page() ?? throw new Exception("Failed to return page.");

    }


}
