using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Dtos;
using ContactService.Domain.Enums;
using ContactService.Domain.Core.Exceptions;
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
    public class GetContactsHandler : IRequestHandler<GetContacts, Response<PagedData<ContactDto>>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetContactsHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<PagedData<ContactDto>>> Handle(GetContacts request, CancellationToken cancellationToken)
        {
            var response = new Response<PagedData<ContactDto>>();
            var contacts = await _contactRepository.GetAsync(request);
            if (contacts == null || contacts.Count() == 0)
            {
                throw new BusinessException(ErrorMessage.NotFound);
            }
            else
            {
                response.Data = new PagedData<ContactDto>
                {
                    Items = _mapper.Map<IEnumerable<ContactDto>>(contacts),
                    Count = await _contactRepository.CountAsync(request),
                    TotalCount = await _contactRepository.CountAsync()
                };
            }
            return response;
        }
    }
}
