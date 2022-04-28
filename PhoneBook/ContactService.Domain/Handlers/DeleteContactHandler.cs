using AutoMapper;
using ContactService.Domain.Core.ResponseBases;
using ContactService.Domain.Core.Exceptions;
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
    public class DeleteContactHandler : IRequestHandler<DeleteContact, Response>
    {
        private readonly IContactRepository _contactRepository;
        private readonly DeleteContactValidator _deleteContactValidator;
        private readonly IMapper _mapper;
        public DeleteContactHandler(
            IContactRepository contactRepository,
            DeleteContactValidator deleteContactValidator,
            IMapper mapper)
        {
            _contactRepository = contactRepository;
            _deleteContactValidator = deleteContactValidator;
            _mapper = mapper;
        }
        public async Task<Response> Handle(DeleteContact request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 200
            };
            var validate = _deleteContactValidator.Validate(request);
            if (validate != null && !validate.IsValid)
            {
                var validations = new Dictionary<string, List<string>>();
                validate.Errors.GroupBy(a => a.PropertyName).ToList().ForEach(a => validations.Add(a.Key, a.Select(b => b.ErrorMessage).ToList()));
                throw new ValidationException(validations);
            }
            await _contactRepository.DeleteAsync(request.ContactId);
            return response;
        }
    }
}
