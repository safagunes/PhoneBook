using Moq;
using ReportService.Domain.Services;
using ReportService.Domain.Services.Contact.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Domain.Core.Extentions;
using ReportService.Domain.Services.Contact.Enums;

namespace ReportService.Test.Mocks
{
    public static class MockContactService
    {
        public static Mock<IContactService> GetContactService()
        {
            var contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    Id = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                    FirstName = "Safa",
                    LastName = "Güneş",
                    Company = "LCW"
                },
                new ContactDto
                {
                    Id = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                    FirstName = "Serdar",
                    LastName = "Güneş",
                    Company = "MEB"
                }
            };

            var contactDetails = new List<ContactDetailDto>
            {
                new ContactDetailDto
                {
                    Id = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                    FirstName = "Safa",
                    LastName = "Güneş",
                    Company = "LCW",
                    ContactInfos = new List<ContactInfoDetailDto>
                    {
                        new ContactInfoDetailDto
                        {
                            Id = Guid.Parse("1b83a91d-0559-46f3-8256-a54fc0ba4db5"),
                            ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                            Type = ContactInfoType.PhoneNumber,
                            Content = "123456789"
                        },
                        new ContactInfoDetailDto
                        {
                            Id = Guid.Parse("3509dc0b-6b71-4a60-bdbb-b21550b74a35"),
                            ContactId = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                            Type = ContactInfoType.Location,
                            Content = "istanbul"
                        }
                    }
                },
                new ContactDetailDto
                {
                    Id = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                    FirstName = "Serdar",
                    LastName = "Güneş",
                    Company = "MEB",
                    ContactInfos = new List<ContactInfoDetailDto>
                    {
                        new ContactInfoDetailDto
                        {
                            Id = Guid.Parse("1588452f-2803-4888-a14c-a5906e9b0780"),
                            ContactId = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                            Type = ContactInfoType.PhoneNumber,
                            Content = "987654321"
                        },
                        new ContactInfoDetailDto
                        {
                            Id = Guid.Parse("582e703b-aac6-4668-8fbf-dd7ffed8a3cf"),
                            ContactId = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                            Type = ContactInfoType.Location,
                            Content = "ankara"
                        }
                    }
                }
            };

            var mockRepo = new Mock<IContactService>();

          

          

          

            mockRepo.Setup(r => r.GetContactsAsync(It.IsAny<Domain.Services.Contact.Requests.GetContacts>())).ReturnsAsync((Domain.Services.Contact.Requests.GetContacts request) =>
            {
                var queryable = contacts.AsQueryable();

                if (!string.IsNullOrEmpty(request.FirstName))
                    queryable = queryable.Where(a => a.FirstName.ToLower().Contains(request.FirstName.ToLower()));
                if (!string.IsNullOrEmpty(request.LastName))
                    queryable = queryable.Where(a => a.LastName.ToLower().Contains(request.LastName.ToLower()));
                if (!string.IsNullOrEmpty(request.Company))
                    queryable = queryable.Where(a => a.Company.ToLower().Contains(request.Company.ToLower()));

                return queryable.OrderBy(request.OrderBy, request.IsAscending)
                                      .Skip(request.PageIndex.Value * request.PageSize.Value)
                                      .Take(request.PageSize.Value).ToList();
            });

            mockRepo.Setup(r => r.GetContactDetailAsync(It.IsAny<Domain.Services.Contact.Requests.GetContact>())).ReturnsAsync((Domain.Services.Contact.Requests.GetContact request) =>
            {
                return contactDetails.FirstOrDefault(a => a.Id == request.ContactId);
            });

            return mockRepo;
        }
    }
}
