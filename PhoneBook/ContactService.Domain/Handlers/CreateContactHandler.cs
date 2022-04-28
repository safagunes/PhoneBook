using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Handlers
{

    public class CreateContactHandler : IRequestHandler<CreateContact, Response>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public CreateContactHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 201
            };
            //TODO: Burada validation uygulanabilir.
            await _contactRepository.AddAsync(_mapper.Map<Contact>(request));
            return response;
        }
    }
}
