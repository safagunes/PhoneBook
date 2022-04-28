using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Core.ResponseBases
{
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; } = 200;
    }
}
