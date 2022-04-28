using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Dtos;
using ContactService.Domain.Enums;
using ContactService.Domain.Exceptions;
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
    public class GetContactHandler : IRequestHandler<GetContact, Response<ContactDetailDto>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetContactHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<ContactDetailDto>> Handle(GetContact request, CancellationToken cancellationToken)
        {
            var response = new Response<ContactDetailDto>();

            var contact = await _contactRepository.GetAsync(request.ContactId);

            if (contact == null)
                throw new BusinessException(ErrorMessage.NotFound);

            response.Data = _mapper.Map<ContactDetailDto>(contact);
            return response;
        }
    }
}
