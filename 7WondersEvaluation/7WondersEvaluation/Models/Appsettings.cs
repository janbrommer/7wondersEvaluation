public class AzureConfiguration
{
    public required CustomVisionConfiguration CustomVision { get; set; }
    public required BlobStorageConfiguration BlobStorage { get; set; }
}

public class CustomVisionConfiguration
{
    public required string PredictionKey { get; set; }
    public required string PredictionUrl { get; set; }
    public required string ProkectId { get; set; }
    public required string IterationName { get; set; }
}

public class BlobStorageConfiguration
{
    public required string ConnectionString { get; set; }
    public required string ContainerName { get; set; }
}
