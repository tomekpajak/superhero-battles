using System;

namespace Shb.Api.Wrappers
{
    public class RequestPagination
    {
        public RequestPagination()
        {            
        }
        public RequestPagination(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
