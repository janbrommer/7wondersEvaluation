using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json; // For the ReadFromJsonAsync method
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;



namespace _7WondersEvaluation.Pages;

public class IndexModel : PageModel
{

    private readonly CustomVisionClient _customVisionClient;    
    private readonly ILogger<IndexModel> _logger;

    public string? ImagePath { get; private set; }
    public ApiResult? Result { get; private set; }
    public EvaluationData? Evaluation { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, CustomVisionClient customVisionClient)
    {
        _logger = logger;
        _customVisionClient = customVisionClient;
    }

    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostAsync()
    {        
        var image = Request.Form.Files.GetFile("image");

        if (image != null && image.Length > 0)
        {
            var imagePath = Path.Combine("uploads", image.FileName);
            using (var stream = new FileStream(Path.Combine("wwwroot", imagePath), FileMode.Create))
            {
                SaveImagetoBlob saveImagetoBlob = new SaveImagetoBlob();
                await image.CopyToAsync(stream);                
                await saveImagetoBlob.UploadAsync(stream,image.FileName);
            }

            ImagePath = "/" + imagePath;
            Result = await _customVisionClient.PredictImageAsync(image.OpenReadStream()) ?? throw new Exception("Failed to get result.");
            CalcVictoryPoints calcVictoryPoints = new CalcVictoryPoints(Result);
            Evaluation = calcVictoryPoints.createEvaluationData();
            Console.WriteLine(Result);
        }        

        return Page() ?? throw new Exception("Failed to return page.");
    }

}
