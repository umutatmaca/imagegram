using Imagegram.Domain.Services;
using SixLabors.ImageSharp;
using System.IO;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.File
{
    public class ImageConverter : IImageConverter
    {
        public async Task ConvertToJpgStreamAsync(Stream sourceStream, Stream destinationStream)
        {
            using (var image = await Image.LoadAsync(sourceStream))
            {
                await image.SaveAsJpegAsync(destinationStream);
            }
        }
    }
}
