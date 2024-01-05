﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json; // For the ReadFromJsonAsync method
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata;



namespace _7WondersEvaluation.Pages;

public class EvaluateModel : PageModel
{

    private readonly CustomVisionClient _customVisionClient;    
    private readonly ILogger<EvaluateModel> _logger;

    private readonly BlobClient _blobClient;

    public string? ImagePath { get; private set; }
    public ApiResult? Result { get; private set; }
    public EvaluationData? Evaluation { get; private set; }
    
    GameContext _context;

    private PlayersInGame playersInGame { get;  set; }

    public EvaluateModel(ILogger<EvaluateModel> logger, CustomVisionClient customVisionClient, BlobClient blobClient, GameContext context)
    {
        _logger = logger;
        _customVisionClient = customVisionClient;        
        _blobClient = blobClient;
        _context = context;
    }

    public void OnGet(int playerId, int gameId)
    {
        playersInGame = _context.PlayersInGame.Where(pg => pg.GameId == gameId && pg.PlayerId == playerId).FirstOrDefault();
    }
    public async Task<IActionResult> OnPostAsync()
    {     
        try 
        {
            var image = Request.Form.Files.GetFile("image");

            if (image != null && image.Length > 0)
            {
                var imagePath = Path.Combine("uploads", image.FileName);
                using (var stream = new FileStream(Path.Combine("wwwroot", imagePath), FileMode.Create))
                {                
                    await image.CopyToAsync(stream);    
                    stream.Position =0;
                    await _blobClient.UploadAsync(stream,image.FileName);
                }

                ImagePath = "/" + imagePath;
                Result = await _customVisionClient.PredictImageAsync(image.OpenReadStream()) ?? throw new Exception("Failed to get result.");
                CalcVictoryPoints calcVictoryPoints = new CalcVictoryPoints(Result);
                Evaluation = calcVictoryPoints.createEvaluationData();
                Console.WriteLine(Result);
            }                    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }   
        return Page() ?? throw new Exception("Failed to return page.");
        
    }

}