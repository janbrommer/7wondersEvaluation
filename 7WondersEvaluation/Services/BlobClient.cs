using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
public class BlobClient
{// Azure Storage-Konto-Verbindungszeichenfolge

    BlobContainerClient _containerClient;

    private readonly AzureConfiguration _azureConfig;


    public BlobClient(IOptions<AzureConfiguration> azureOptions)
    {
        _azureConfig = azureOptions.Value;
        // Create a BlobServiceClient using the connection string    
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(_azureConfig.BlobStorage.ConnectionString));
        // Get a reference to the container
        _containerClient = blobServiceClient.GetBlobContainerClient(_azureConfig.BlobStorage.ContainerName);
    }

    public async Task UploadAsync(FileStream imageStream, string blobName)
    {
        // Upload a blob from a local file
        Azure.Storage.Blobs.BlobClient blobClient = _containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(imageStream, true);
    }

}
