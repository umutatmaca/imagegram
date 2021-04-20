using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Imagegram.Domain.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.File
{
    public class AzureBlobFileUploader : IFileUploader
    {
        private readonly BlobServiceClient blobServiceClient;
        private const string ImageContainerName = "images";
        public AzureBlobFileUploader(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// upload given stream to azure blob storage
        /// </summary>
        /// <param name="folderName">folder name to place image</param>
        /// <param name="fileName">file name with extension</param>
        /// <param name="contentType">content type / related to extension</param>
        /// <param name="fileStream">source stream</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>azure blob file path</returns>
        public async Task<string> UploadAsync(string folderName, string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken)
        {
            var containerClient = GetContainerClient(ImageContainerName);
            var blobClient = containerClient.GetBlobClient($"{folderName}/{fileName}");
            await blobClient.UploadAsync(content: fileStream, httpHeaders: new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

            return blobClient.Uri.AbsoluteUri;
        }

        public void DeleteFolder(string folderName)
        {
            var containerClient = GetContainerClient(ImageContainerName);
            var blobClient = containerClient.GetBlobClient(folderName);
            blobClient.DeleteIfExistsAsync();
        }

        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    }
}
