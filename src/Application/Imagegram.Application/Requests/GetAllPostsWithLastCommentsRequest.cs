using Imagegram.Application.Responses;
using MediatR;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// fetch posts with last comments query
    /// </summary>
    public class GetAllPostsWithLastCommentsRequest : IRequest<GetAllPostsWithLastCommentsResponse>
    {
        /// <summary>
        /// page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// page number
        /// </summary>
        public int PageNumber { get; }
        public GetAllPostsWithLastCommentsRequest(int pageSize = 50, int pageNumber = 1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
