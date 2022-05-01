using AutoMapper;
using ContactService.Domain;
using ContactService.Domain.Handlers;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
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
    public class GetContactsHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> mockRepo;
        
        public GetContactsHandlerTest()
        {
            mockRepo = MockContactRepository.GetContactRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task Get_Contacts()
        {
            var handler = new GetContactsHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetContacts{}, CancellationToken.None);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task Get_Contacts_By_Name()
        {
            var handler = new GetContactsHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetContacts
            {
                FirstName = "safa"
            }, CancellationToken.None);
            Assert.Equal(1, result.Data.Count);
        }
    }
}
