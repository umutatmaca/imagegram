using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imagegram.Domain.Services
{
    public interface IImageUploader
    {
        Task<string> UploadAsync(Guid postId, IFormFile imageFile, CancellationToken cancellationToken);
    }
}
