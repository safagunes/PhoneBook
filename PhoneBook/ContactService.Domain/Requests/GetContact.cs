using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Requests
{
    public class GetContact:IRequest<Response<ContactDetailDto>>
    {
        public Guid ContactId { get; set; }
    }
}
