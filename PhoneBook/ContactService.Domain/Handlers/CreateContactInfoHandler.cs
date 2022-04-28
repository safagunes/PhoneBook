using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Exceptions;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
using ContactService.Domain.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Handlers
{
    public class CreateContactInfoHandler : IRequestHandler<CreateContactInfo, Response>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly CreateContactInfoValidator _createContactInfoValidator;
        private readonly IMapper _mapper;
        public CreateContactInfoHandler(
            IContactInfoRepository contactInfoRepository,
            CreateContactInfoValidator createContactInfoValidator,
            IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _createContactInfoValidator = createContactInfoValidator;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateContactInfo request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 201
            };
            var validate = _createContactInfoValidator.Validate(request);
            if (validate != null && !validate.IsValid)
            {
                var validations = new Dictionary<string, List<string>>();
                validate.Errors.GroupBy(a => a.PropertyName).ToList().ForEach(a => validations.Add(a.Key, a.Select(b => b.ErrorMessage).ToList()));
                throw new ValidationException(validations);
            }
            await _contactInfoRepository.AddAsync(_mapper.Map<ContactInfo>(request));
            return response;
        }
    }
}
