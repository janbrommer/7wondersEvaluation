using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class CustomVisionClient
{
    private readonly HttpClient _httpClient;
    private readonly string _predictionKey;

    public CustomVisionClient(HttpClient httpClient, string predictionKey)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _predictionKey = predictionKey ?? throw new ArgumentNullException(nameof(predictionKey));
    }

    public async Task<ApiResult?> PredictImageAsync(
        string projectId,
        string publishedName,
        Stream imageStream,
        int? numTagsPerBoundingBox = 1,
        string? application = null)
    {
        if (imageStream == null) throw new ArgumentNullException(nameof(imageStream));
        if (publishedName == null) throw new ArgumentNullException(nameof(publishedName));
        string your_endpoint = "exercise-jbr2.cognitiveservices.azure.com";        
        // Construct the URL.
        string url = $"https://{your_endpoint}/customvision/v3.1/Prediction/{projectId}/detect/iterations/{publishedName}/image";
        if (numTagsPerBoundingBox.HasValue)
        {
            url += $"?numTagsPerBoundingBox={numTagsPerBoundingBox.Value}";
        }
        if (!string.IsNullOrEmpty(application))
        {
            url += (url.Contains("?") ? "&" : "?") + $"application={application}";
        }

        // Prepare the HTTP request.
        using var content = new StreamContent(imageStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        _httpClient.DefaultRequestHeaders.Add("Prediction-key", _predictionKey);
        
        // Send the HTTP request and get the response.
        using var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();                
        // Deserialize the response.
        // Note: Ensure you have a class (ApiResult) matching the response schema.
        var apiResult = await response.Content.ReadFromJsonAsync<ApiResult>();
        if (apiResult != null)
        {
            return filterResult(apiResult, 0.7);
        }else 
        {
            throw new Exception("Kein gültiges Ergebnis vorhanden."); // Hier wird eine benutzerdefinierte Exception ausgelöst
        }         
    }

    private ApiResult filterResult(ApiResult apiResult, double minProbability)
    {
        List<Prediction> predictions = apiResult.Predictions ?? new List<Prediction>();
        List<Prediction> filteredPredictions = new List<Prediction>();
        if (predictions.Count == 0)
        {
            return apiResult;
        }
        foreach (var prediction in predictions)
        {
            if (prediction.Probability > minProbability)
            {
                filteredPredictions.Add(prediction);
            }
        }
        apiResult.Predictions = filteredPredictions;
        return apiResult;
    }

}

