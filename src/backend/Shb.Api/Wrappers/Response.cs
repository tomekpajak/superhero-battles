using System;
using System.Collections.Generic;

namespace Shb.Api.Wrappers
{
    public class Response<T> where T : class
    {
        public Response() 
        {            
        }
        public Response(T data)
        {
            this.Data = data;            
            this.Succeeded = true;
            this.Errors = new List<string>();
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
        public string Message { get; set; }        
    }
}
