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
    public class CreateContactHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> mockRepo;
        private readonly CreateContactValidator _createContactValidator;


        public CreateContactHandlerTest()
        {
            mockRepo = MockContactRepository.GetContactRepository();
            _createContactValidator = new CreateContactValidator();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Create_Contact()
        {
            var handler = new CreateContactHandler(mockRepo.Object, _createContactValidator, _mapper);
            var result = await handler.Handle(new CreateContact
            {
                FirstName = "Ali",
                LastName = "Güneş",
                Company = "LCW"
            },CancellationToken.None);

            var contacts = await mockRepo.Object.GetAsync(new GetContacts { });
            Assert.Equal(3, contacts.Count());
        }

        [Fact]
        public async Task Create_Contact_ThrowsValidationException()
        {
            var handler = new CreateContactHandler(mockRepo.Object, _createContactValidator, _mapper);

            Func<Task> act = () =>  handler.Handle(new CreateContact
            {
                //FirstName = "Ali",
                LastName = "Güneş",
                Company = "LCW"
            }, CancellationToken.None); 

            var exception = await Assert.ThrowsAsync<ValidationException>(act);
            Assert.True(exception.ValidationErrors.ContainsKey("FirstName"));
        }
    }
}
