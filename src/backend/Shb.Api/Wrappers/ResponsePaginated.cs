using System;

namespace Shb.Api.Wrappers
{
    public class ResponsePaginated<T> : Response<T> where T : class
    {
        public ResponsePaginated(T data, int pageNumber, int totalsPages) : base(data)
        {
            this.PageNumber = pageNumber;
            this.TotalPages = totalsPages;
        }

        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
