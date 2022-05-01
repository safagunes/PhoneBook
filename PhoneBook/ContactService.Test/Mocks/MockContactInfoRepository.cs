using ContactService.Domain.Enums;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Mocks
{
    public static class MockContactInfoRepository
    {
        public static Mock<IContactInfoRepository> GetContactInfoRepository()
        {
            var contactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    Id = Guid.Parse("1b83a91d-0559-46f3-8256-a54fc0ba4db5"),
                    ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                    Type = ContactInfoType.PhoneNumber,
                    Content = "123456789"
                },
                new ContactInfo
                {
                    Id = Guid.Parse("3509dc0b-6b71-4a60-bdbb-b21550b74a35"),
                    ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                    Type = ContactInfoType.Location,
                    Content = "istanbul"
                },
                new ContactInfo
                {
                    Id = Guid.Parse("1588452f-2803-4888-a14c-a5906e9b0780"),
                    ContactId = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                    Type = ContactInfoType.PhoneNumber,
                    Content = "987654321"
                },
                new ContactInfo
                {
                    Id = Guid.Parse("582e703b-aac6-4668-8fbf-dd7ffed8a3cf"),
                    ContactId = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                    Type = ContactInfoType.Location,
                    Content = "ankara"
                }
            };

            var mockRepo = new Mock<IContactInfoRepository>();


            mockRepo.Setup(r => r.AddAsync(It.IsAny<ContactInfo>())).ReturnsAsync((ContactInfo contact) =>
            {
                contactInfos.Add(contact);
                return contact;
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Callback((Guid contactId) =>
            {
                var contact = contactInfos.FirstOrDefault(a => a.Id == contactId);
                if (contact != null)
                {
                    contactInfos.Remove(contact);
                }
            });

            return mockRepo;
        }

    }
}
