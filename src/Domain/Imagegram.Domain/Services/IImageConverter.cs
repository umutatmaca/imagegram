using System.IO;
using System.Threading.Tasks;

namespace Imagegram.Domain.Services
{
    public interface IImageConverter
    {
        Task ConvertToJpgStreamAsync(Stream sourceStream, Stream destinationStream);
    }
}
