public class ApiResult
{
    public Guid Id { get; set; }
    public Guid Project { get; set; }
    public Guid Iteration { get; set; }
    public string? Created { get; set; } // Made the property nullable
    public List<Prediction>? Predictions { get; set; } // Made the property nullable
}

public class Prediction
{
    public double Probability { get; set; }
    public Guid TagId { get; set; }
    public string? TagName { get; set; }
    public BoundingBox? BoundingBox { get; set; }
    public string? TagType { get; set; }
}

public class BoundingBox
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}