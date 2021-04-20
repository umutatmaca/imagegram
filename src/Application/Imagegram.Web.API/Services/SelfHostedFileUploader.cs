using Imagegram.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Web.API.Services
{
    public class SelfHostedFileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageConverter imageConverter;

        public SelfHostedFileUploader(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IImageConverter imageConverter)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.imageConverter = imageConverter;
        }

        /// <summary>
        /// upload given stream to self hosted web root
        /// </summary>
        /// <param name="folderName">folder name to place image</param>
        /// <param name="fileName">file name with extension</param>
        /// <param name="contentType">content type / related to extension</param>
        /// <param name="fileStream">source stream</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>local file path</returns>
        public async Task<string> UploadAsync(string folderName, string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken)
        {
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "Content", "Images", folderName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            using (var newSaveStream = File.Create(Path.Combine(uploadPath, fileName)))
            {

                fileStream.Seek(0, SeekOrigin.Begin);
                await imageConverter.ConvertToJpgStreamAsync(fileStream, newSaveStream);
                fileStream.Close();
            }

            return $"{GetBaseUri()}/Content/Images/{folderName}/{fileName}";
        }

        public void DeleteFolder(string folderName)
        {
            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "Content", "Images", folderName);
            if (Directory.Exists(uploadPath))
            {
                Directory.Delete(uploadPath, true);
            }
        }

        private string GetBaseUri()
        {
            if (httpContextAccessor.HttpContext == null)
            {
                return string.Empty;
            }

            return $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}";
        }
    }
}
