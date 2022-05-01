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
    public class DeleteContactHandlerTest
    {

        private readonly Mock<IContactRepository> mockRepo;
        private readonly DeleteContactValidator _deleteContactValidator;

        public DeleteContactHandlerTest()
        {
            mockRepo = MockContactRepository.GetContactRepository();
            _deleteContactValidator = new DeleteContactValidator();
        }
        [Fact]
        public async Task Delete_Existing_Contact()
        {
            var handler = new DeleteContactHandler(mockRepo.Object, _deleteContactValidator);
            var result = await handler.Handle(new DeleteContact
            {
                ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb")
            }, CancellationToken.None);

            var contact = await mockRepo.Object.GetAsync(Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"));
            Assert.True(contact == null);
        }
        [Fact]
        public async Task Delete_Existing_Contact_ThrowsValidationException()
        {
            var handler = new DeleteContactHandler(mockRepo.Object, _deleteContactValidator);

            Func<Task> act = () => handler.Handle(new DeleteContact
            {
                ContactId = null
            }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ValidationException>(act);
            Assert.True(exception.ValidationErrors.ContainsKey("ContactId"));

        }
    }
}
