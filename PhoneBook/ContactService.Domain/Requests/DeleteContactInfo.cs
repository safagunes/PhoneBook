using ContactService.Domain.Core.ResponseBases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Requests
{
    public class DeleteContactInfo : IRequest<Response>
    {
        public Guid ContactInfoId { get; set; }
    }
}
