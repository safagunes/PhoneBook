using AutoMapper;
using ContactService.Domain;
using ContactService.Domain.Core.Exceptions;
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
    public class DeleteContactInfoHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactInfoRepository> mockRepo;
        private readonly DeleteContactInfoValidator _deleteContactInfoValidator;

        public DeleteContactInfoHandlerTest()
        {
            mockRepo = MockContactInfoRepository.GetContactInfoRepository();
            _deleteContactInfoValidator = new DeleteContactInfoValidator();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Delete_Existing_Contact_Info()
        {
            var handler = new DeleteContactInfoHandler(mockRepo.Object, _deleteContactInfoValidator, _mapper);
            var result = await handler.Handle(new DeleteContactInfo
            {
                ContactInfoId = Guid.Parse("1b83a91d-0559-46f3-8256-a54fc0ba4db5")
            }, CancellationToken.None);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Delete_Existing_Contact_Info_ThrowsValidationException()
        {
            var handler = new DeleteContactInfoHandler(mockRepo.Object, _deleteContactInfoValidator, _mapper);

            Func<Task> act = () => handler.Handle(new DeleteContactInfo
            {
                ContactInfoId = null
            }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ValidationException>(act);
            Assert.True(exception.ValidationErrors.ContainsKey("ContactInfoId"));

        }
    }
}
