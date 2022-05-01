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
    public class GetContactHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IContactRepository> mockRepo;

        public GetContactHandlerTest()
        {
            mockRepo = MockContactRepository.GetContactRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Get_Contact_By_Id()
        {
            var handler = new GetContactHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetContact { ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb") }, CancellationToken.None);
            Assert.True(result.Data.FirstName.ToLower()=="safa");
        }
    }

   
}
