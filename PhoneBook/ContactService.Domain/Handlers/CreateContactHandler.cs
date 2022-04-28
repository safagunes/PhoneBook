using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Core.Exceptions;
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

    public class CreateContactHandler : IRequestHandler<CreateContact, Response>
    {
        private readonly IContactRepository _contactRepository;
        private readonly CreateContactValidator _createContactValidator;
        private readonly IMapper _mapper;
        public CreateContactHandler(
            IContactRepository contactRepository,
            CreateContactValidator createContactValidator,
            IMapper mapper)
        {
            _contactRepository = contactRepository;
            _createContactValidator = createContactValidator;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 201
            };
            var validate = _createContactValidator.Validate(request);
            if (validate != null && !validate.IsValid)
            {
                var validations = new Dictionary<string, List<string>>();
                validate.Errors.GroupBy(a => a.PropertyName).ToList().ForEach(a => validations.Add(a.Key, a.Select(b => b.ErrorMessage).ToList()));
                throw new ValidationException(validations);
            }
            await _contactRepository.AddAsync(_mapper.Map<Contact>(request));
            return response;
        }
    }
}
