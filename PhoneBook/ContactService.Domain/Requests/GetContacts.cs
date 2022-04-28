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
    public class GetContacts : PagedQuery, IRequest<Response<PagedData<ContactDto>>>
    {
        public override string OrderBy { get; set; } = "Id";

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
