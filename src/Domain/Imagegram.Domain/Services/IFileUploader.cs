using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Domain.Services
{
    public interface IFileUploader
    {
        /// <summary>
        /// upload given stream to related storage system
        /// </summary>
        /// <param name="folderName">folder name to place image</param>
        /// <param name="fileName">file name with extension</param>
        /// <param name="contentType">content type / related to extension</param>
        /// <param name="fileStream">source stream</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>uploaded file path</returns>
        Task<string> UploadAsync(string folderName, string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken);

        /// <summary>
        /// deletes the folder
        /// </summary>
        /// <param name="folderName"></param>
        void DeleteFolder(string folderName);
    }
}
