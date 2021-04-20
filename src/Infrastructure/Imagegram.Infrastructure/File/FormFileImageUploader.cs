using Imagegram.Domain.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.File
{
    public class FormFileImageUploader : IImageUploader
    {
        private readonly IFileUploader fileUploader;
        public FormFileImageUploader(IFileUploader fileUploader)
        {
            this.fileUploader = fileUploader;
        }

        public async Task<string> UploadAsync(Guid postId, IFormFile imageFile, CancellationToken cancellationToken)
        {
            var fileName = NormalizeFileName($"{Guid.NewGuid()}_{imageFile.FileName}");
            using (var fileStream = imageFile.OpenReadStream())
            {
                fileName = await fileUploader.UploadAsync(postId.ToString(), fileName, imageFile.ContentType, fileStream, cancellationToken);
            }

            return fileName;
        }

        private static string NormalizeFileName(string fileName)
        {
            return $"{fileName.Substring(0, fileName.LastIndexOf("."))}.jpg";
        }
    }
}
