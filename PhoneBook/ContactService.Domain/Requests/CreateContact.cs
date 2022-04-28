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
    public class CreateContact: IRequest<Response>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set; }
    }
}
