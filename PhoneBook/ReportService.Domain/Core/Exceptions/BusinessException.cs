﻿using ReportService.Domain.Core.Extentions;
using ReportService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(int code, string message, Exception innerException = null) : base(
            message, innerException)
        {
            this.Code = code;
        }

        public BusinessException(ErrorMessage errorMessage, Exception innerException = null) : base(
            errorMessage.GetDescription(), innerException)
        {
            this.Code = (int)errorMessage;
        }
        public int Code { get; set; }
    }
}
