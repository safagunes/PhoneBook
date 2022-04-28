using ContactService.Domain.Core.ResponseBases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Requests
{
    public class DeleteContact : IRequest<Response>
    {
        public Guid ContactId { get; set; }
    }
}
