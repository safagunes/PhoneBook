using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Core.ResponseBases
{
    public class ResponseOfValidation : Response
    {
        public ResponseOfValidation()
        {
            Success = false;
            Code = 422;
        }
        public Dictionary<string, List<string>> ValidationErrors { get; set; }
    }
}
