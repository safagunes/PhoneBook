using ContactService.Domain.Core.Extentions;
using ContactService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Core.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException(int code, string message, Exception innerException = null) : base(
            message, innerException)
        {
            this.Code = code;
        }

        public TransactionException(ErrorMessage errorMessage, Exception innerException = null) : base(
            errorMessage.GetDescription(), innerException)
        {
            this.Code = (int)errorMessage;
        }
        public int Code { get; set; }
    }
}
