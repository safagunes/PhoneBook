using ContactService.Domain.Enums;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
using ContactService.Domain.Core.Extentions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Mocks
{
    public static class MockContactRepository
    {
        public static Mock<IContactRepository> GetContactRepository()
        {
            var contacts = new List<Contact>
            {
                new Contact
                {
                    Id = Guid.Parse("12be55c9-f77a-4d18-bfd2-26363d2553eb"),
                    FirstName = "Safa",
                    LastName = "Güneş",
                    Company = "LCW",
                    ContactInfos = new List<ContactInfo>
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
                        }
                    }
                },
                new Contact
                {
                    Id = Guid.Parse("27fb2bde-4b24-450d-83a7-da312ae21d89"),
                    FirstName = "Serdar",
                    LastName = "Güneş",
                    Company = "MEB",
                    ContactInfos = new List<ContactInfo>
                    {
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
                    }
                }
            };

            var mockRepo = new Mock<IContactRepository>();


            mockRepo.Setup(r => r.AddAsync(It.IsAny<Contact>())).ReturnsAsync((Contact contact) =>
            {
                contacts.Add(contact);
                return contact;
            });

            mockRepo.Setup(r => r.CountAsync(It.IsAny<GetContacts>())).ReturnsAsync((GetContacts request) =>
            {
                var queryable = contacts.AsQueryable();

                if (!string.IsNullOrEmpty(request.FirstName))
                    queryable = queryable.Where(a => a.FirstName.ToLower().Contains(request.FirstName.ToLower()));
                if (!string.IsNullOrEmpty(request.LastName))
                    queryable = queryable.Where(a => a.LastName.ToLower().Contains(request.LastName.ToLower()));
                if (!string.IsNullOrEmpty(request.Company))
                    queryable = queryable.Where(a => a.Company.ToLower().Contains(request.Company.ToLower()));

                return queryable.Count();
            });

            mockRepo.Setup(r => r.CountAsync()).ReturnsAsync(contacts.Count);


            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Callback((Guid contactId) =>
            {
                var contact = contacts.FirstOrDefault(a=>a.Id==contactId);
                if (contact != null)
                {
                    contacts.Remove(contact);
                }
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<GetContacts>())).ReturnsAsync((GetContacts request) =>
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

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid contactId) =>
            {
                return contacts.FirstOrDefault(a => a.Id == contactId);
            });

            return mockRepo;
        }
    }
}
