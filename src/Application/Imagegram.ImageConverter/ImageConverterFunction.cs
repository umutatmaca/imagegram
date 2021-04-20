using Imagegram.Domain.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Imagegram.ImageConverter
{
    public class ImageConverterFunction
    {
        private readonly IImageConverter imageConverter;

        public ImageConverterFunction(IImageConverter imageConverter)
        {
            this.imageConverter = imageConverter;
        }

        [FunctionName("ConvertBmpToJpg")]
        public async Task RunForBmp(
            [BlobTrigger("images/{name}", Connection = "AzureWebJobsStorage")]
            Stream sourceImageStream,
            [Blob("images/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")]
            Stream destinationImageStream,
            string name, ILogger log)
        {
            await imageConverter.ConvertToJpgStreamAsync(sourceImageStream, destinationImageStream);

            log.LogInformation($"Processed blob Name:{name}. Size: {sourceImageStream.Length} Bytes");
        }
    }
}
