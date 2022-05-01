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
    public class DeleteContactInfoHandler : IRequestHandler<DeleteContactInfo, Response>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly DeleteContactInfoValidator _deleteContactInfoValidator;
        private readonly IMapper _mapper;
        public DeleteContactInfoHandler(
            IContactInfoRepository contactInfoRepository,
            DeleteContactInfoValidator deleteContactInfoValidator,
            IMapper mapper)
        {
            _contactInfoRepository = contactInfoRepository;
            _deleteContactInfoValidator = deleteContactInfoValidator;
            _mapper = mapper;
        }
        public async Task<Response> Handle(DeleteContactInfo request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 200
            };
            var validate = _deleteContactInfoValidator.Validate(request);
            if (validate != null && !validate.IsValid)
            {
                var validations = new Dictionary<string, List<string>>();
                validate.Errors.GroupBy(a => a.PropertyName).ToList().ForEach(a => validations.Add(a.Key, a.Select(b => b.ErrorMessage).ToList()));
                throw new ValidationException(validations);
            }
            await _contactInfoRepository.DeleteAsync(request.ContactInfoId.Value);
            return response;
        }
    }
}
