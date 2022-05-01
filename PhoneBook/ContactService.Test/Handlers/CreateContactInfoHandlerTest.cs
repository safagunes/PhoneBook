using AutoMapper;
using ContactService.Domain;
using ContactService.Domain.Core.Exceptions;
using ContactService.Domain.Enums;
using ContactService.Domain.Handlers;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
using ContactService.Domain.Validations;
using ContactService.Test.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ContactService.Test.Handlers
{
    public class CreateContactInfoHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactInfoRepository> mockRepo;
        private readonly CreateContactInfoValidator _createContactInfoValidator;


        public CreateContactInfoHandlerTest()
        {
            mockRepo = MockContactInfoRepository.GetContactInfoRepository();
            _createContactInfoValidator = new CreateContactInfoValidator();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Create_Contact_Info()
        {
            var handler = new CreateContactInfoHandler(mockRepo.Object, _createContactInfoValidator, _mapper);
            var response = await handler.Handle(new CreateContactInfo
            {
                ContactId = Guid.NewGuid(),
                Type = ContactInfoType.Location,
                Content = "Aydın"
            }, CancellationToken.None);

            Assert.True(response.Success);
        }

        [Fact]
        public async Task Create_Contact_Info_ThrowsValidationException()
        {
            var handler = new CreateContactInfoHandler(mockRepo.Object, _createContactInfoValidator, _mapper);

            Func<Task> act = () => handler.Handle(new CreateContactInfo
            {
                //ContactId = Guid.NewGuid(),
                Type = ContactInfoType.Location,
                Content = "Aydın"
            }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ValidationException>(act);
            Assert.True(exception.ValidationErrors.ContainsKey("ContactId"));
        }
    }
}
