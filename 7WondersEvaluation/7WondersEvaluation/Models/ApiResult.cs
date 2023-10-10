public class ApiResult
{
    public Guid Id { get; set; }
    public Guid Project { get; set; }
    public Guid Iteration { get; set; }
    public string? Created { get; set; } // Made the property nullable
    public List<Prediction>? Predictions { get; set; } // Made the property nullable
    public override string ToString()
    {
        return $"Id: {Id}, Project: {Project}, Iteration: {Iteration}, Created: {Created}, Predictions: {PredictionsToString()}";
    }

    private string PredictionsToString()
    {
        if (Predictions == null)
        {
            return "null";
        }

        List<string> predictionStrings = new List<string>();

        foreach (var prediction in Predictions)
        {
            string boundingBoxString = prediction.BoundingBox != null
                ? $"BoundingBox: Left={prediction.BoundingBox.Left}, Top={prediction.BoundingBox.Top}, Width={prediction.BoundingBox.Width}, Height={prediction.BoundingBox.Height}"
                : "BoundingBox: null";

            string predictionString = $"Probability: {prediction.Probability}, TagId: {prediction.TagId}, TagName: {prediction.TagName}, TagType: {prediction.TagType}, {boundingBoxString}";

            predictionStrings.Add(predictionString);
        }

        return "[" + string.Join(", ", predictionStrings) + "]";
    }
}

public class Prediction
{
    public double Probability { get; set; }
    public Guid TagId { get; set; }
    public string TagName { get; set; }
    public BoundingBox BoundingBox { get; set; }
    public string? TagType { get; set; }
}

public class BoundingBox
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}