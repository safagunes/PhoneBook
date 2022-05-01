using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Requests
{
    public class CreateContactInfo : IRequest<Response>
    {
        public Guid? ContactId { get; set; }
        public ContactInfoType? Type { get; set; }
        public string? Content { get; set; }
    }
}
