using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
public class SaveImagetoBlob{// Azure Storage-Konto-Verbindungszeichenfolge
    string connectionString = "https://7wondersimages.blob.core.windows.net/uploads?sp=racw&st=2023-10-12T20:02:58Z&se=2023-10-31T05:02:58Z&spr=https&sv=2022-11-02&sr=c&sig=aQvTDsHBre%2F6rIueIc3Zqu4edMisX5uN0R2cmX3UUPw%3D";
    string containerName = "uploads";    

    BlobContainerClient containerClient;


    public SaveImagetoBlob()
    {
        // Create a BlobServiceClient using the connection string    
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(connectionString));
        // Get a reference to the container
        containerClient = blobServiceClient.GetBlobContainerClient(containerName);    
    }

    public async Task UploadAsync(FileStream imageStream, string blobName)
    {
        // Upload a blob from a local file
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(imageStream, true);        
    }

}
