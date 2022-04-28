using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Core.ResponseBases
{
    public class ResponseOfException : Response
    {
        public ResponseOfException()
        {
            Success = false;
            Code = 400;
        }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
